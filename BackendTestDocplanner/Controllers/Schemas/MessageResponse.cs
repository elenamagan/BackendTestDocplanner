using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace BackendTestDocplanner.Controllers.Schemas
{
    /// <summary>
    /// Represents an error response of the API
    /// </summary>
    public class MessageResponse
    {
        /// <summary>
        /// The error/success message
        /// </summary>
        [Required]
        [SwaggerSchema(Description = "The error/success message", Nullable = false)]
        public string Message { get; set; }

        /// <summary>
        /// The error/success details
        /// </summary>
        [Required]
        [SwaggerSchema(Description = "The error/success details", Nullable = true)]
        public string Details { get; set; }

        public MessageResponse(string message, string details)
        {
            Message = message;
            Details = details;
        }
    }
}
