using BackendTestDocplanner.Services.Slot.Models;

namespace BackendTestDocplanner.Controllers.Helpers
{
    public static class SlotHelper
    {

        /// <summary>
        /// Returns only the available slots of a certain day
        /// </summary>
        public static List<Slot> CalculateAvailableSlots(DateTime baseDate, DailyAvailability? dailyAvailability, int slotDurationMinutes)
        {
            if (dailyAvailability == null || baseDate < DateTime.Today)
            {
                return new List<Slot>();
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
        public static List<Slot> GenerateSlots(DateTime baseDate, WorkPeriod workPeriod, int slotDurationMinutes)
        {
            var slots = new List<Slot>();

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
        public static List<Slot> GenerateSlotsForPeriod(DateTime start, DateTime end, int slotDurationMinutes)
        {
            var slots = new List<Slot>();
            while (start < end)
            {
                DateTime slotEnd = start.AddMinutes(slotDurationMinutes);
                if (slotEnd <= end)
                {
                    slots.Add(new Slot(start, slotEnd));
                }
                start = slotEnd;
            }
            return slots;
        }

        /// <summary>
        /// Compares a list of possible slots with another of busy slots, and returns only the slots that are not busy
        /// </summary>
        public static List<Slot> GetAvailableSlots(List<Slot> possibleSlots, Slot[]? busySlots)
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
    }
}
