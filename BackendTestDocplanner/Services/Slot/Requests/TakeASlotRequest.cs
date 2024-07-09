using BackendTestDocplanner.Services.Slot.Models;

namespace BackendTestDocplanner.Services.Models.Requests
{
    /// <summary>
    /// Defines the request "Take a slot" of the slot service
    /// </summary>
    internal class TakeASlotRequest
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
        public PatientModel Patient { get; set; }

        public TakeASlotRequest(string start, string end, string comments, PatientModel patient)
        {
            Start = start;
            End = end;
            Comments = comments;
            Patient = patient;
        }
    }
}
