using BackendTestDocplanner.Controllers.Helpers;
using BackendTestDocplanner.Controllers.Models;
using BackendTestDocplanner.TestUI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BackendTestDocplanner
{
    public partial class WeeklySlots : Form
    {

        private readonly HttpClient _httpClient;
        private readonly DataGridView _dataGridView;

        private DateTime _currentDate = DateTime.Today;

        public WeeklySlots()
        {
            InitializeComponent();
            _httpClient = new HttpClient { BaseAddress = new Uri("https://localhost:5001") };

            // Initialize DataGridView
            _dataGridView = new DataGridView
            {
                Dock = DockStyle.Fill,
                ColumnCount = 7,
                ColumnHeadersVisible = true
            };

            // Set column headers
            _dataGridView.Columns[0].Name = "Monday";
            _dataGridView.Columns[1].Name = "Tuesday";
            _dataGridView.Columns[2].Name = "Wednesday";
            _dataGridView.Columns[3].Name = "Thursday";
            _dataGridView.Columns[4].Name = "Friday";
            _dataGridView.Columns[5].Name = "Saturday";
            _dataGridView.Columns[6].Name = "Sunday";

            _dataGridView.ReadOnly = true;
            _dataGridView.CellClick += dataGridView_CellClick;

            this.Controls.Add(_dataGridView);

            this.Load += async (s, e) => await LoadWeeklySlotsAsync();
        }

        private async Task LoadWeeklySlotsAsync()
        {
            try
            {
                _currentDate = DateTime.Today;
                await LoadSlotsForDate(_currentDate);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private async void BtnPreviousWeek_Click(object sender, EventArgs e)
        {
            _currentDate = _currentDate.AddDays(-7);
            await LoadSlotsForDate(_currentDate);

            UpdatePreviousWeekButtonState();

            titleLabel.Text = $"Available slots from {SlotHelper.GetWeekStartDate(_currentDate).ToString("dd/MM/yyyy")} to {SlotHelper.GetWeekEndDate(_currentDate).ToString("dd/MM/yyyy")}";
        }

        private async void BtnNextWeek_Click(object sender, EventArgs e)
        {
            _currentDate = _currentDate.AddDays(7);
            await LoadSlotsForDate(_currentDate);

            UpdatePreviousWeekButtonState();

            titleLabel.Text = $"Available slots from {SlotHelper.GetWeekStartDate(_currentDate).ToString("dd/MM/yyyy")} to {SlotHelper.GetWeekEndDate(_currentDate).ToString("dd/MM/yyyy")}";
        }
        private void UpdatePreviousWeekButtonState()
        {
            var previousWeekDate = _currentDate.AddDays(-7);

            if (previousWeekDate < DateTime.Today)
            {
                btnPreviousWeek.Enabled = false;
            }
            else
            {
                btnPreviousWeek.Enabled = true;
            }
        }


        private async Task LoadSlotsForDate(DateTime date)
        {
            try
            {
                var dateString = date.ToString("yyyyMMdd");
                var response = await _httpClient.GetAsync($"Slot/GetAvailableSlots?date={dateString}");
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<WeeklyAvailableSlots>(responseBody);

                DisplayAvailableSlots(result);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void DisplayAvailableSlots(WeeklyAvailableSlots slots)
        {
            _dataGridView.Rows.Clear();
            var maxRows = Math.Max(slots.Monday.Count, Math.Max(slots.Tuesday.Count,
                        Math.Max(slots.Wednesday.Count, Math.Max(slots.Thursday.Count,
                        Math.Max(slots.Friday.Count, Math.Max(slots.Saturday.Count, slots.Sunday.Count))))));

            for (int i = 0; i < maxRows; i++)
            {
                _dataGridView.Rows.Add();
                _dataGridView.Rows[i].Cells[0].Value = i < slots.Monday.Count ? slots.Monday[i].Start.ToString("HH:mm") : "";
                _dataGridView.Rows[i].Cells[1].Value = i < slots.Tuesday.Count ? slots.Tuesday[i].Start.ToString("HH:mm") : "";
                _dataGridView.Rows[i].Cells[2].Value = i < slots.Wednesday.Count ? slots.Wednesday[i].Start.ToString("HH:mm") : "";
                _dataGridView.Rows[i].Cells[3].Value = i < slots.Thursday.Count ? slots.Thursday[i].Start.ToString("HH:mm") : "";
                _dataGridView.Rows[i].Cells[4].Value = i < slots.Friday.Count ? slots.Friday[i].Start.ToString("HH:mm") : "";
                _dataGridView.Rows[i].Cells[5].Value = i < slots.Saturday.Count ? slots.Saturday[i].Start.ToString("HH:mm") : "";
                _dataGridView.Rows[i].Cells[6].Value = i < slots.Sunday.Count ? slots.Sunday[i].Start.ToString("HH:mm") : "";
            }
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verificar si se hizo clic en una celda válida y si tiene un valor
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                var cellValue = _dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                if (cellValue != null && !string.IsNullOrEmpty(cellValue.ToString()))
                {
                    // Obtener la fecha y hora de inicio del slot seleccionado
                    string startDateTime = _dataGridView.Columns[e.ColumnIndex].HeaderText + " " + _dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

                    // Abrir el formulario TakeSlotForm
                    using (var takeSlotForm = new TakeSlotForm(startDateTime))
                    {
                        var result = takeSlotForm.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            // Mostrar mensaje de confirmación
                            MessageBox.Show("Slot taken successfully");
                        }
                    }
                }
            }
        }
    }
}