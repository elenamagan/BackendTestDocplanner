using BackendTestDocplanner.Controllers.Helpers;
using BackendTestDocplanner.Controllers.Schemas;
using BackendTestDocplanner.Services.Slot;
using BackendTestDocplanner.Services.Slot.Schemas;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using System.Globalization;

namespace BackendTestDocplanner.Controllers
{
    /// <summary>
    /// Exposes data to the front application
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class SlotController : ControllerBase
    {
        private readonly SlotService _slotService;

        public SlotController(SlotService slotService)
        {
            _slotService = slotService;
        }

        #region API endpoints

        /// <summary>
        /// Gets the available slots for the week of the provided date
        /// </summary>
        /// <param name="date">The date to get the week's available slots in yyyyMMdd format</param>
        /// <returns>The available slots for the week</returns>
        /// <response code="200">Returns the available slots for the week</response>
        /// <response code="400">If the request is invalid</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpGet("GetAvailableSlots")]
        [ProducesResponseType(typeof(WeeklyAvailableSlots), 200)]
        [ProducesResponseType(typeof(MessageResponse), 400)]
        [ProducesResponseType(typeof(MessageResponse), 500)]
        public async Task<IActionResult> GetAvailableSlotsAsync(
            [FromQuery, SwaggerParameter(Description = "Any date in yyyyMMdd format, used to get the available slots for the week that includes this date.")] string date
        )
        {
            if (!DateTime.TryParseExact(date, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var providedDate))
            {
                return BadRequest("Invalid date format. Please use yyyyMMdd.");
            }

            DateTime weekStartDate = SlotHelper.GetWeekStartDate(providedDate);
            string currentWeekStartDate = weekStartDate.ToString("yyyyMMdd");

            try
            {
                // Call the GetWeeklyAvailabilityAsync method with the week's start date
                var response = await _slotService.GetWeeklyAvailabilityAsync(currentWeekStartDate);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var availability = JsonConvert.DeserializeObject<FacilityWeeklyAvailability>(responseBody)!;

                    var availableSlots = new WeeklyAvailableSlots(
                        availability.Facility.FacilityId,
                        SlotHelper.CalculateAvailableSlots(weekStartDate, availability.Monday, availability.SlotDurationMinutes),
                        SlotHelper.CalculateAvailableSlots(weekStartDate.AddDays(1), availability.Tuesday, availability.SlotDurationMinutes),
                        SlotHelper.CalculateAvailableSlots(weekStartDate.AddDays(2), availability.Wednesday, availability.SlotDurationMinutes),
                        SlotHelper.CalculateAvailableSlots(weekStartDate.AddDays(3), availability.Thursday, availability.SlotDurationMinutes),
                        SlotHelper.CalculateAvailableSlots(weekStartDate.AddDays(4), availability.Friday, availability.SlotDurationMinutes),
                        SlotHelper.CalculateAvailableSlots(weekStartDate.AddDays(5), availability.Saturday, availability.SlotDurationMinutes),
                        SlotHelper.CalculateAvailableSlots(weekStartDate.AddDays(6), availability.Sunday, availability.SlotDurationMinutes)
                    );

                    return Ok(availableSlots);
                }
                else
                {
                    // Read the response body for details
                    string errorDetails = response.ReasonPhrase!;
                    string responseBody = await response.Content.ReadAsStringAsync();
                    if (responseBody != null)
                    {
                        errorDetails += ": " + responseBody;
                    }
                    var errorResponse = StatusCode((int)response.StatusCode, new MessageResponse("Failed to get weekly availability", errorDetails));
                    return errorResponse;
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new MessageResponse("An error occurred", ex.Message));
            }
        }

        /// <summary>
        /// Takes a slot with the provided details
        /// </summary>
        /// <param name="request">The request containing slot details and patient information</param>
        /// <returns>The response from the slot service</returns>
        /// <response code="200">If the slot was successfully taken</response>
        /// <response code="400">If the request is invalid</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpPost("TakeSlot")]
        [ProducesResponseType(typeof(MessageResponse), 200)]
        [ProducesResponseType(typeof(MessageResponse), 400)]
        [ProducesResponseType(typeof(MessageResponse), 500)]
        public async Task<IActionResult> TakeSlotAsync([FromBody] TakeSlotRequest request)
        {
            try
            {
                // Call the TakeSlotAsync method in the SlotService
                HttpResponseMessage response = await _slotService.TakeSlotAsync(request);

                // If the response is successful, return OK
                if (response.IsSuccessStatusCode)
                {
                    return Ok(new { Message = "Slot successfully taken", Details = request.ToString() });
                }

                // Read the response body for details
                string errorDetails = response.ReasonPhrase!;
                string responseBody = await response.Content.ReadAsStringAsync();
                if (responseBody != null)
                {
                    errorDetails += ": " + responseBody;
                }
                var errorResponse = StatusCode((int)response.StatusCode, new MessageResponse("Failed to take slot", errorDetails));
                return errorResponse;
            }
            catch (Exception ex)
            {
                // If there is an exception, return the error message
                return StatusCode(500, new MessageResponse("An error occurred", ex.Message));
            }
        }

        #endregion
    }
}
