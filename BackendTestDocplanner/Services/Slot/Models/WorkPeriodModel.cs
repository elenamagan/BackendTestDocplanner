namespace BackendTestDocplanner.Services.Slot.Models
{
    /// <summary>
    /// Defines working hours as provided in the slot service
    /// </summary>
    internal class WorkPeriodModel
    {
        /// <summary>
        /// Morning opening hour (int, from 0 to 23)
        /// </summary>
        public int StartHour { get; set; }

        /// <summary>
        /// Morning closing hour (int, from 0 to 23)
        /// </summary>
        public int LunchStartHour { get; set; }

        /// <summary>
        /// Afternoon opening hour (int, from 0 to 23)
        /// </summary>
        public int LunchEndHour { get; set; }

        /// <summary>
        /// Afternoon closing hour (int, from 0 to 23)
        /// </summary>
        public int EndHour { get; set; }

        public WorkPeriodModel(int startHour, int lunchStartHour, int lunchEndHour, int endHour)
        {
            StartHour = startHour;
            LunchStartHour = lunchStartHour;
            LunchEndHour = lunchEndHour;
            EndHour = endHour;
        }
    }
}
