namespace BackendTestDocplanner.Services.Slot.Models
{
    /// <summary>
    /// Defines the request "Take a slot" of the slot service
    /// </summary>
    public class TakeSlotRequest
    {
        /// <summary>
        /// Initial time of the slot. For simplicity, it doesn't consider timezones.
        /// Format YYYY-MM-dd HH:mm:ss
        /// </summary>
        public string Start { get; set; }

        /// <summary>
        /// End time of the slot. For simplicity, it doesn't consider timezones.
        /// Format YYYY-MM-dd HH:mm:ss
        /// </summary>
        public string End { get; set; }

        /// <summary>
        /// Additional instructions for the doctor
        /// </summary>
        public string Comments { get; set; }

        /// <summary>
        /// Patient information
        /// </summary>
        public Patient Patient { get; set; }

        public TakeSlotRequest(string start, string end, string comments, Patient patient)
        {
            Start = start;
            End = end;
            Comments = comments;
            Patient = patient;
        }
    }
}
