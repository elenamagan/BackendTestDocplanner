namespace BackendTestDocplanner.Services.Slot.Models
{
    /// <summary>
    /// Defines a daily slot availability as provided in the slot service
    /// </summary>
    public class DailyAvailabilityModel
    {
        /// <summary>
        /// Work period
        /// </summary>
        public WorkPeriodModel WorkPeriod { get; set; }

        /// <summary>
        /// List of busy slots within the work period
        /// </summary>
        public SlotModel[]? BusySlots { get; set; }

        public DailyAvailabilityModel(WorkPeriodModel workPeriod, SlotModel[]? busySlots)
        {
            WorkPeriod = workPeriod;
            BusySlots = busySlots;
        }
    }
}
