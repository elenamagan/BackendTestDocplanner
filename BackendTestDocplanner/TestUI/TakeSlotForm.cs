using BackendTestDocplanner.Services.Slot.Schemas;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;

namespace BackendTestDocplanner.TestUI
{
    public partial class TakeSlotForm : Form
    {
        private readonly string _startDateTime;
        private readonly string _endDateTime;
        private readonly string _facilityId;
        private readonly string _baseUrl;

        public TakeSlotForm(string facilityId, string startDateTime, string endDateTime)
        {
            InitializeComponent();
            _facilityId = facilityId;
            _startDateTime = startDateTime;
            _endDateTime = endDateTime;

            // Display the start and end date and time of the selected slot
            lblStartDateTime.Text = $"Start Date and Time: {_startDateTime}";
            lblEndDateTime.Text = $"End Date and Time: {_endDateTime}";

            // Load configuration and get the base URL
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            _baseUrl = configuration["Kestrel:Endpoints:Http:Url"] ?? "https://localhost:5001";

        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            // Get the values entered by the user
            string comments = txtComments.Text;
            string name = txtName.Text;
            string secondName = txtSecondName.Text;
            string email = txtEmail.Text;
            string phone = txtPhone.Text;

            // Create the PatientModel object
            Patient patient = new Patient(name, secondName, email, phone);

            // Create the TakeASlotRequest object
            TakeSlotRequest takeSlotRequest = new TakeSlotRequest(_facilityId, _startDateTime, _endDateTime, comments, patient);

            // Call the TakeSlotAsync method from the controller
            HttpClient _httpClient = new HttpClient { BaseAddress = new Uri(_baseUrl) };
            string jsonContent = JsonConvert.SerializeObject(takeSlotRequest);
            StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync($"Slot/TakeSlot", content);

            if (response.IsSuccessStatusCode)
            {
                // Display confirmation message
                MessageBox.Show("Slot succesfully taken:\n" +
                                $"Facility Id: {takeSlotRequest.FacilityId}\n" +
                                $"Start Date and Time: {takeSlotRequest.Start}\n" +
                                $"End Date and Time: {takeSlotRequest.End}\n" +
                                $"Comments: {takeSlotRequest.Comments}\n" +
                                $"Patient: {takeSlotRequest.Patient.Name} {takeSlotRequest.Patient.SecondName}\n" +
                                $"Email: {takeSlotRequest.Patient.Email}\n" +
                                $"Phone: {takeSlotRequest.Patient.Phone}");

                // Close the form with DialogResult OK to indicate that the operation was successful
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                // Handle error
                MessageBox.Show("Failed to save slot information.");
            }
        }
    }
}
