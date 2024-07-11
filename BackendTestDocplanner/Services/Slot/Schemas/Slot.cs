using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace BackendTestDocplanner.Services.Slot.Schemas
{
    /// <summary>
    /// Defines a slot as provided in the slot service
    /// </summary>
    public class Slot
    {
        /// <summary>
        /// Initial time of the slot. For simplicity, it doesn't consider timezones. Format yyyy-MM-dd HH:mm:ss
        /// </summary>
        [Required]
        [SwaggerSchema(Description = "Initial time of the slot. For simplicity, it doesn't consider timezones. Format yyyy-MM-dd HH:mm:ss", Nullable = false)]
        public DateTime Start { get; set; }

        /// <summary>
        /// End time of the slot. For simplicity, it doesn't consider timezones. Format yyyy-MM-dd HH:mm:ss
        /// </summary>
        [Required]
        [SwaggerSchema(Description = "End time of the slot. For simplicity, it doesn't consider timezones. Format yyyy-MM-dd HH:mm:ss", Nullable = false)]
        public DateTime End { get; set; }

        public Slot(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }
    }
}