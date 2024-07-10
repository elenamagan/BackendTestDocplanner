using BackendTestDocplanner.Services.Slot.Models;

namespace BackendTestDocplanner.Services.Models.Responses
{
    /// <summary>
    /// Defines the response "Weekly Available Slots" of the slot controller
    /// </summary>
    public class WeeklyAvailableSlots
    {
        /// <summary>
        /// Slot availability on monday
        /// </summary>
        public List<SlotModel> Monday { get; set; }

        /// <summary>
        /// Slot availability on monday
        /// </summary>
        public List<SlotModel> Tuesday { get; set; }

        /// <summary>
        /// Slot availability on monday
        /// </summary>
        public List<SlotModel> Wednesday { get; set; }

        /// <summary>
        /// Slot availability on monday
        /// </summary>
        public List<SlotModel> Thursday { get; set; }

        /// <summary>
        /// Slot availability on monday
        /// </summary>
        public List<SlotModel> Friday { get; set; }

        /// <summary>
        /// Slot availability on monday
        /// </summary>
        public List<SlotModel> Saturday { get; set; }

        /// <summary>
        /// Slot availability on monday
        /// </summary>
        public List<SlotModel> Sunday { get; set; }

        public WeeklyAvailableSlots(List<SlotModel> monday, List<SlotModel> tuesday, List<SlotModel> wednesday, List<SlotModel> thursday, List<SlotModel> friday, List<SlotModel> saturday, List<SlotModel> sunday)
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
