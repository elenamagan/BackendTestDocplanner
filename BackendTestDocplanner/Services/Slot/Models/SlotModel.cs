namespace BackendTestDocplanner.Services.Slot.Models
{
    /// <summary>
    /// Defines a slot as provided in the slot service
    /// </summary>
    public class SlotModel
    {
        /// <summary>
        /// Initial time of the slot. For simplicity, it doesn't consider timezones.
        /// </summary>
        public DateTime Start { get; set; }

        /// <summary>
        /// End time of the slot. For simplicity, it doesn't consider timezones.
        /// </summary>
        public DateTime End { get; set; }

        public SlotModel(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }
    }
}
