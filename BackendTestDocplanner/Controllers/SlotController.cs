using BackendTestDocplanner.Services.Models.Responses;
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
            DateTime weekStartDate = GetWeekStartDate(today);
            string currentWeekStartDate = weekStartDate.ToString("yyyyMMdd");

            // Call the GetWeeklyAvailabilityAsync method with the current week's start date
            var availability = await _slotService.GetWeeklyAvailabilityAsync(currentWeekStartDate);

            var availableSlots = new WeeklyAvailableSlots(
                CalculateAvailableSlots(weekStartDate, availability.Monday, availability.SlotDurationMinutes),
                CalculateAvailableSlots(weekStartDate.AddDays(1), availability.Tuesday, availability.SlotDurationMinutes),
                CalculateAvailableSlots(weekStartDate.AddDays(2), availability.Wednesday, availability.SlotDurationMinutes),
                CalculateAvailableSlots(weekStartDate.AddDays(3), availability.Thursday, availability.SlotDurationMinutes),
                CalculateAvailableSlots(weekStartDate.AddDays(4), availability.Friday, availability.SlotDurationMinutes),
                CalculateAvailableSlots(weekStartDate.AddDays(5), availability.Saturday, availability.SlotDurationMinutes),
                CalculateAvailableSlots(weekStartDate.AddDays(6), availability.Sunday, availability.SlotDurationMinutes)
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

            DateTime weekStartDate = GetWeekStartDate(providedDate);
            string currentWeekStartDate = weekStartDate.ToString("yyyyMMdd");

            // Call the GetWeeklyAvailabilityAsync method with the week's start date
            var availability = await _slotService.GetWeeklyAvailabilityAsync(currentWeekStartDate);

            var availableSlots = new WeeklyAvailableSlots(
                CalculateAvailableSlots(weekStartDate, availability.Monday, availability.SlotDurationMinutes),
                CalculateAvailableSlots(weekStartDate.AddDays(1), availability.Tuesday, availability.SlotDurationMinutes),
                CalculateAvailableSlots(weekStartDate.AddDays(2), availability.Wednesday, availability.SlotDurationMinutes),
                CalculateAvailableSlots(weekStartDate.AddDays(3), availability.Thursday, availability.SlotDurationMinutes),
                CalculateAvailableSlots(weekStartDate.AddDays(4), availability.Friday, availability.SlotDurationMinutes),
                CalculateAvailableSlots(weekStartDate.AddDays(5), availability.Saturday, availability.SlotDurationMinutes),
                CalculateAvailableSlots(weekStartDate.AddDays(6), availability.Sunday, availability.SlotDurationMinutes)
            );

            return Ok(availableSlots);
        }

        #endregion


        #region Helper functions (could be in separate static class)

        /// <summary>
        /// Returns only the available slots of a certain day
        /// </summary>
        private static List<SlotModel> CalculateAvailableSlots(DateTime baseDate, DailyAvailabilityModel? dailyAvailability, int slotDurationMinutes)
        {
            if (dailyAvailability == null)
            {
                return new List<SlotModel>();
            }

            var possibleSlots = GenerateSlots(baseDate, dailyAvailability.WorkPeriod, slotDurationMinutes);
            return GetAvailableSlots(possibleSlots, dailyAvailability.BusySlots);
        }

        /// <summary>
        /// Gets the start date (monday) of a week, given a certain date
        /// </summary>
        public static DateTime GetWeekStartDate(DateTime date)
        {
            // Assuming the week starts on Monday
            int diff = (7 + (date.DayOfWeek - DayOfWeek.Monday)) % 7;
            DateTime weekStart = date.AddDays(-diff);
            return weekStart;
        }

        /// <summary>
        /// Gets the start date (monday) of a week, given a certain date
        /// </summary>
        public static DateTime GetWeekEndDate(DateTime date)
        {
            DateTime startOfWeek = GetWeekStartDate(date);
            DateTime endOfWeek = startOfWeek.AddDays(6);
            return endOfWeek;
        }

        /// <summary>
        /// Generates a list of all possible slots of a daily work period
        /// </summary>
        public static List<SlotModel> GenerateSlots(DateTime baseDate, WorkPeriodModel workPeriod, int slotDurationMinutes)
        {
            var slots = new List<SlotModel>();

            DateTime startMorning = baseDate.AddHours(workPeriod.StartHour);
            DateTime endMorning = baseDate.AddHours(workPeriod.LunchStartHour);
            DateTime startAfternoon = baseDate.AddHours(workPeriod.LunchEndHour);
            DateTime endAfternoon = baseDate.AddHours(workPeriod.EndHour);

            // Generate morning slots
            slots.AddRange(GenerateSlotsForPeriod(startMorning, endMorning, slotDurationMinutes));

            // Generate afternoon slots
            slots.AddRange(GenerateSlotsForPeriod(startAfternoon, endAfternoon, slotDurationMinutes));

            return slots;
        }

        /// <summary>
        /// Generates a list of all possible slots given the slot duration and a certain start and end time
        /// </summary>
        private static List<SlotModel> GenerateSlotsForPeriod(DateTime start, DateTime end, int slotDurationMinutes)
        {
            var slots = new List<SlotModel>();
            while (start < end)
            {
                DateTime slotEnd = start.AddMinutes(slotDurationMinutes);
                if (slotEnd <= end)
                {
                    slots.Add(new SlotModel(start, slotEnd));
                }
                start = slotEnd;
            }
            return slots;
        }

        /// <summary>
        /// Compares a list of possible slots with another of busy slots, and returns only the slots that are not busy
        /// </summary>
        public static List<SlotModel> GetAvailableSlots(List<SlotModel> possibleSlots, SlotModel[]? busySlots)
        {
            if (busySlots == null)
            {
                return possibleSlots;
            }

            return possibleSlots.Where(possibleSlot =>
                !busySlots.Any(busySlot =>
                    busySlot.Start < possibleSlot.End && busySlot.End > possibleSlot.Start))
                .ToList();
        }

        #endregion
    }
}
