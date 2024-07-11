namespace BackendTestDocplanner.Services.Slot.Schemas
{
    /// <summary>
    /// Defines a daily slot availability as provided in the slot service
    /// </summary>
    public class DailyAvailability
    {
        /// <summary>
        /// Work period
        /// </summary>
        public WorkPeriod WorkPeriod { get; set; }

        /// <summary>
        /// List of busy slots within the work period
        /// </summary>
        public Slot[]? BusySlots { get; set; }

        public DailyAvailability(WorkPeriod workPeriod, Slot[]? busySlots)
        {
            WorkPeriod = workPeriod;
            BusySlots = busySlots;
        }
    }
}
