using BackendTestDocplanner.Controllers.Helpers;
using BackendTestDocplanner.Controllers.Models;
using BackendTestDocplanner.Services.Slot;
using BackendTestDocplanner.Services.Slot.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Globalization;
using System.Text;

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
        /// Devuelve el mensaje "Hello World"
        /// </summary>
        [HttpGet("helloWorld")]
        public async Task<IActionResult> HelloWorldAsync()
        {
            // Get the current week's start date
            DateTime today = DateTime.Today;
            DateTime weekStartDate = SlotHelper.GetWeekStartDate(today);
            string currentWeekStartDate = weekStartDate.ToString("yyyyMMdd");

            // Call the GetWeeklyAvailabilityAsync method with the current week's start date
            var availability = await _slotService.GetWeeklyAvailabilityAsync(currentWeekStartDate);

            var availableSlots = new WeeklyAvailableSlots(
                SlotHelper.CalculateAvailableSlots(weekStartDate, availability.Monday, availability.SlotDurationMinutes),
                SlotHelper.CalculateAvailableSlots(weekStartDate.AddDays(1), availability.Tuesday, availability.SlotDurationMinutes),
                SlotHelper.CalculateAvailableSlots(weekStartDate.AddDays(2), availability.Wednesday, availability.SlotDurationMinutes),
                SlotHelper.CalculateAvailableSlots(weekStartDate.AddDays(3), availability.Thursday, availability.SlotDurationMinutes),
                SlotHelper.CalculateAvailableSlots(weekStartDate.AddDays(4), availability.Friday, availability.SlotDurationMinutes),
                SlotHelper.CalculateAvailableSlots(weekStartDate.AddDays(5), availability.Saturday, availability.SlotDurationMinutes),
                SlotHelper.CalculateAvailableSlots(weekStartDate.AddDays(6), availability.Sunday, availability.SlotDurationMinutes)
            );

            return Ok(new { Message = "Hello World", AvailableSlots = availableSlots });
        }

        /// <summary>
        /// Gets the available slots for the week of the provided date
        /// </summary>
        /// <param name="date">The date to get the week's available slots in yyyyMMdd format</param>
        /// <returns>The available slots for the week</returns>
        [HttpGet("GetAvailableSlots")]
        public async Task<IActionResult> GetAvailableSlotsAsync([FromQuery] string date)
        {
            if (!DateTime.TryParseExact(date, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var providedDate))
            {
                return BadRequest("Invalid date format. Please use yyyyMMdd.");
            }

            DateTime weekStartDate = SlotHelper.GetWeekStartDate(providedDate);
            string currentWeekStartDate = weekStartDate.ToString("yyyyMMdd");

            // Call the GetWeeklyAvailabilityAsync method with the week's start date
            var availability = await _slotService.GetWeeklyAvailabilityAsync(currentWeekStartDate);

            var availableSlots = new WeeklyAvailableSlots(
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

        #endregion
    }
}
