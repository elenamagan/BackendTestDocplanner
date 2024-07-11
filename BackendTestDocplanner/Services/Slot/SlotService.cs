using BackendTestDocplanner.Services.Slot.Schemas;
using Newtonsoft.Json;
using System.Text;

namespace BackendTestDocplanner.Services.Slot
{
    public class SlotService
    {
        private readonly HttpClient _httpClient;

        public SlotService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// Gets weekly availability from the slot service API
        /// </summary>
        /// <param name="date">The date in yyyyMMdd format. Must be start of the week (monday)</param>
        /// <returns>The availability response</returns>
        public async Task<HttpResponseMessage> GetWeeklyAvailabilityAsync(string date)
        {
            string requestUri = $"api/availability/GetWeeklyAvailability/{date}";
            HttpResponseMessage response = await _httpClient.GetAsync(requestUri);
            return response;
        }

        /// <summary>
        /// Takes a slot by sending a POST request to the slot service API
        /// </summary>
        /// <param name="takeSlotRequest">The request containing slot details and patient information</param>
        /// <returns>The response from the API</returns>
        public async Task<HttpResponseMessage> TakeSlotAsync(TakeSlotRequest takeSlotRequest)
        {
            string requestUri = "api/availability/TakeSlot";
            string jsonContent = JsonConvert.SerializeObject(takeSlotRequest);
            StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync(requestUri, content);
            return response;
        }
    }
}
