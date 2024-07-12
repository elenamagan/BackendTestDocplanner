using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace BackendTestDocplanner.Services.Slot.Schemas
{
    /// <summary>
    /// Defines a patient as provided in the slot service
    /// </summary>
    public class Patient
    {
        /// <summary>
        /// Patient name
        /// </summary>
        [Required]
        [SwaggerSchema(Description = "Patient name", Nullable = false)]
        public string Name { get; set; }

        /// <summary>
        /// Patient second name
        /// </summary>
        [Required]
        [SwaggerSchema(Description = "Patient second name", Nullable = false)]
        public string SecondName { get; set; }

        /// <summary>
        /// Patient email
        /// </summary>
        [Required]
        [EmailAddress]
        [SwaggerSchema(Description = "Patient email", Nullable = false)]
        public string Email { get; set; }

        /// <summary>
        /// Patient phone
        /// </summary>
        [Required]
        [Phone]
        [SwaggerSchema(Description = "Patient phone", Nullable = false)]
        public string Phone { get; set; }

        public Patient(string name, string secondName, string email, string phone)
        {
            Name = name;
            SecondName = secondName;
            Email = email;
            Phone = phone;
        }

		public override string ToString()
		{
			return $"Patient(Name: {Name}, SecondName: {SecondName}, Email: {Email}, Phone: {Phone})";
		}
	}
}
