using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.ComponentModel.DataAnnotations;

namespace BackendTestDocplanner.Services.Slot.Schemas
{
    /// <summary>
    /// Defines the request "Take a slot" of the slot service
    /// </summary>
    public class TakeSlotRequest
    {
        /// <summary>
        /// Facility id
        /// </summary>
        [Required]
        [SwaggerSchema(Description = "Facility id", Nullable = false)]
        public string FacilityId { get; set; }

        /// <summary>
        /// Initial time of the slot. For simplicity, it doesn't consider timezones.
        /// Format YYYY-MM-dd HH:mm:ss
        /// </summary>
        [Required]
        [SwaggerSchema(Description = "Initial time of the slot. For simplicity, it doesn't consider timezones. Format YYYY-MM-dd HH:mm:ss", Nullable = false)]
        public string Start { get; set; }

        /// <summary>
        /// End time of the slot. For simplicity, it doesn't consider timezones.
        /// Format YYYY-MM-dd HH:mm:ss
        /// </summary>
        [Required]
        [SwaggerSchema(Description = "End time of the slot. For simplicity, it doesn't consider timezones. Format YYYY-MM-dd HH:mm:ss", Nullable = false)]
        public string End { get; set; }

        /// <summary>
        /// Additional instructions for the doctor
        /// </summary>
        [Required]
        [SwaggerSchema(Description = "Additional instructions for the doctor", Nullable = false)]
        public string Comments { get; set; }

        /// <summary>
        /// Patient information
        /// </summary>
        [Required]
        [SwaggerSchema(Description = "Patient information", Nullable = false)]
        public Patient Patient { get; set; }

        public TakeSlotRequest(string facilityId, string start, string end, string comments, Patient patient)
        {
            FacilityId = facilityId;
            Start = start;
            End = end;
            Comments = comments;
            Patient = patient;
        }
    }

    public class TakeSlotRequestExample : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type == typeof(TakeSlotRequest))
            {
                schema.Example = new OpenApiObject
                {
                    ["FacilityId"] = new OpenApiString("7960f43f-67dc-4bc5-a52b-9a3494306749"),
                    ["Start"] = new OpenApiString("2024-07-12 10:00:00"),
                    ["End"] = new OpenApiString("2024-07-12 10:10:00"),
                    ["Comments"] = new OpenApiString("My back hurts."),
                    ["Patient"] = new OpenApiObject
                    {
                        ["Name"] = new OpenApiString("Elena"),
                        ["SecondName"] = new OpenApiString("Nito"),
                        ["Email"] = new OpenApiString("elena.nito@example.com"),
                        ["Phone"] = new OpenApiString("555 44 33 22")
                    }
                };
            }
        }
    }
}