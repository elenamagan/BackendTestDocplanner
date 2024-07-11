using BackendTestDocplanner.Services.Slot.Schemas;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace BackendTestDocplanner.Controllers.Schemas
{
    /// <summary>
    /// Defines the response "Weekly Available Slots" of the slot controller
    /// </summary>
    public class WeeklyAvailableSlots
    {
        /// <summary>
        /// Facility id
        /// </summary>
        [Required]
        [SwaggerSchema(Description = "Facility id", Nullable = false)]
        public string FacilityId { get; set; }

        /// <summary>
        /// Slot availability on Monday
        /// </summary>
        [Required]
        [SwaggerSchema(Description = "Slot availability on Monday", Nullable = false)]
        public List<Slot> Monday { get; set; }

        /// <summary>
        /// Slot availability on Tuesday
        /// </summary>
        [Required]
        [SwaggerSchema(Description = "Slot availability on Tuesday", Nullable = false)]
        public List<Slot> Tuesday { get; set; }

        /// <summary>
        /// Slot availability on Wednesday
        /// </summary>
        [Required]
        [SwaggerSchema(Description = "Slot availability on Wednesday", Nullable = false)]
        public List<Slot> Wednesday { get; set; }

        /// <summary>
        /// Slot availability on Thursday
        /// </summary>
        [Required]
        [SwaggerSchema(Description = "Slot availability on Thursday", Nullable = false)]
        public List<Slot> Thursday { get; set; }

        /// <summary>
        /// Slot availability on Friday
        /// </summary>
        [Required]
        [SwaggerSchema(Description = "Slot availability on Friday", Nullable = false)]
        public List<Slot> Friday { get; set; }

        /// <summary>
        /// Slot availability on Saturday
        /// </summary>
        [Required]
        [SwaggerSchema(Description = "Slot availability on Saturday", Nullable = false)]
        public List<Slot> Saturday { get; set; }

        /// <summary>
        /// Slot availability on Sunday
        /// </summary>
        [Required]
        [SwaggerSchema(Description = "Slot availability on Sunday", Nullable = false)]
        public List<Slot> Sunday { get; set; }

        public WeeklyAvailableSlots(string facilityId, List<Slot> monday, List<Slot> tuesday, List<Slot> wednesday, List<Slot> thursday, List<Slot> friday, List<Slot> saturday, List<Slot> sunday)
        {
            FacilityId = facilityId;
            Monday = monday ?? new List<Slot>();
            Tuesday = tuesday ?? new List<Slot>();
            Wednesday = wednesday ?? new List<Slot>();
            Thursday = thursday ?? new List<Slot>();
            Friday = friday ?? new List<Slot>();
            Saturday = saturday ?? new List<Slot>();
            Sunday = sunday ?? new List<Slot>();
        }
    }
}