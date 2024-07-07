
using Newtonsoft.Json;

namespace BackendTestDocplanner
{
    public partial class WeeklySlots : Form
    {
        private readonly HttpClient _httpClient;

        public WeeklySlots()
        {
            InitializeComponent();
            _httpClient = new HttpClient { BaseAddress = new Uri("https://localhost:5001") };
        }

        private async void testButton_Click(object sender, EventArgs e)
        {
            try
            {
                var response = await _httpClient.GetAsync("Slot/helloWorld");
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<HelloWorldResponse>(responseBody);
                MessageBox.Show(result?.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        public class HelloWorldResponse
        {
            public string? Message { get; set; }
        }
    }
}
