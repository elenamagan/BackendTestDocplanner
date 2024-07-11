using BackendTestDocplanner.Services.Slot.Models;

namespace BackendTestDocplanner.Controllers.Models
{
    /// <summary>
    /// Defines the response "Weekly Available Slots" of the slot controller
    /// </summary>
    public class WeeklyAvailableSlots
    {
        /// <summary>
        /// Slot availability on monday
        /// </summary>
        public List<Slot> Monday { get; set; }

        /// <summary>
        /// Slot availability on monday
        /// </summary>
        public List<Slot> Tuesday { get; set; }

        /// <summary>
        /// Slot availability on monday
        /// </summary>
        public List<Slot> Wednesday { get; set; }

        /// <summary>
        /// Slot availability on monday
        /// </summary>
        public List<Slot> Thursday { get; set; }

        /// <summary>
        /// Slot availability on monday
        /// </summary>
        public List<Slot> Friday { get; set; }

        /// <summary>
        /// Slot availability on monday
        /// </summary>
        public List<Slot> Saturday { get; set; }

        /// <summary>
        /// Slot availability on monday
        /// </summary>
        public List<Slot> Sunday { get; set; }

        public WeeklyAvailableSlots(List<Slot> monday, List<Slot> tuesday, List<Slot> wednesday, List<Slot> thursday, List<Slot> friday, List<Slot> saturday, List<Slot> sunday)
        {
            Monday = monday;
            Tuesday = tuesday;
            Wednesday = wednesday;
            Thursday = thursday;
            Friday = friday;
            Saturday = saturday;
            Sunday = sunday;
        }
    }
}
