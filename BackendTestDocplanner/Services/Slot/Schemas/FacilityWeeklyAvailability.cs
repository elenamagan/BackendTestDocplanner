namespace BackendTestDocplanner.Services.Slot.Schemas
{
    /// <summary>
    /// Defines the response "Get Availability" of the slot service
    /// </summary>
    public class FacilityWeeklyAvailability
    {
        /// <summary>
        /// Facility information
        /// </summary>
        public Facility Facility { get; set; }

        /// <summary>
        /// Duration of the slot in minutes
        /// </summary>
        public int SlotDurationMinutes { get; set; }

        /// <summary>
        /// Slot availability on monday
        /// </summary>
        public DailyAvailability? Monday { get; set; }

        /// <summary>
        /// Slot availability on monday
        /// </summary>
        public DailyAvailability? Tuesday { get; set; }

        /// <summary>
        /// Slot availability on monday
        /// </summary>
        public DailyAvailability? Wednesday { get; set; }

        /// <summary>
        /// Slot availability on monday
        /// </summary>
        public DailyAvailability? Thursday { get; set; }

        /// <summary>
        /// Slot availability on monday
        /// </summary>
        public DailyAvailability? Friday { get; set; }

        /// <summary>
        /// Slot availability on monday
        /// </summary>
        public DailyAvailability? Saturday { get; set; }

        /// <summary>
        /// Slot availability on monday
        /// </summary>
        public DailyAvailability? Sunday { get; set; }

        public FacilityWeeklyAvailability(Facility facility, int slotDurationMinutes, DailyAvailability? monday, DailyAvailability? tuesday, DailyAvailability? wednesday, DailyAvailability? thursday, DailyAvailability? friday, DailyAvailability? saturday, DailyAvailability? sunday)
        {
            Facility = facility;
            SlotDurationMinutes = slotDurationMinutes;
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
