using BackendTestDocplanner.Services.Slot.Models;
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
        /// <param name="date">The date in yyyyMMdd format. Must be start of the week (monday)</param>
        /// <returns>The availability response</returns>
        public async Task<FacilityWeeklyAvailability> GetWeeklyAvailabilityAsync(string date)
        {
            string requestUri = $"api/availability/GetWeeklyAvailability/{date}";
            HttpResponseMessage response = await _httpClient.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            FacilityWeeklyAvailability availabilityResponse = JsonConvert.DeserializeObject<FacilityWeeklyAvailability>(responseBody);
            return availabilityResponse;
        }
    }
}
