using BackendTestDocplanner.Services.Models.Responses;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;

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
        /// <param name="date">The date in yyyyMMdd format</param>
        /// <returns>The availability response</returns>
        public async Task<GetAvailabilityResponse> GetWeeklyAvailabilityAsync(string date)
        {
            string requestUri = $"api/availability/GetWeeklyAvailability/{date}";
            HttpResponseMessage response = await _httpClient.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            GetAvailabilityResponse availabilityResponse = JsonConvert.DeserializeObject<GetAvailabilityResponse>(responseBody);
            return availabilityResponse;
        }
    }
}
