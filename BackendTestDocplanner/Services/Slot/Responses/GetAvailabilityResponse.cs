using BackendTestDocplanner.Services.Slot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTestDocplanner.Services.Models.Responses
{
    /// <summary>
    /// Defines the response "Get Availability" of the slot service
    /// </summary>
    public class GetAvailabilityResponse
    {
        /// <summary>
        /// Facility information
        /// </summary>
        public FacilityModel Facility { get; set; }

        /// <summary>
        /// Duration of the slot in minutes
        /// </summary>
        public int SlotDurationMinutes { get; set; }

        /// <summary>
        /// Slot availability on monday
        /// </summary>
        public DailyAvailabilityModel? Monday { get; set; }

        /// <summary>
        /// Slot availability on monday
        /// </summary>
        public DailyAvailabilityModel? Tuesday { get; set; }

        /// <summary>
        /// Slot availability on monday
        /// </summary>
        public DailyAvailabilityModel? Wednesday { get; set; }

        /// <summary>
        /// Slot availability on monday
        /// </summary>
        public DailyAvailabilityModel? Thursday { get; set; }

        /// <summary>
        /// Slot availability on monday
        /// </summary>
        public DailyAvailabilityModel? Friday { get; set; }

        /// <summary>
        /// Slot availability on monday
        /// </summary>
        public DailyAvailabilityModel? Saturday { get; set; }

        /// <summary>
        /// Slot availability on monday
        /// </summary>
        public DailyAvailabilityModel? Sunday { get; set; }

        public GetAvailabilityResponse(FacilityModel facility, int slotDurationMinutes, DailyAvailabilityModel? monday, DailyAvailabilityModel? tuesday, DailyAvailabilityModel? wednesday, DailyAvailabilityModel? thursday, DailyAvailabilityModel? friday, DailyAvailabilityModel? saturday, DailyAvailabilityModel? sunday)
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
