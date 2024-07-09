namespace BackendTestDocplanner.Services.Slot.Models
{
    /// <summary>
    /// Defines a patient as provided in the slot service
    /// </summary>
    internal class PatientModel
    {
        /// <summary>
        /// Patient name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Patient second name
        /// </summary>
        public string SecondName { get; set; }

        /// <summary>
        /// Patient email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Patient phone
        /// </summary>
        public string Phone { get; set; }

        public PatientModel(string name, string secondName, string email, string phone)
        {
            Name = name;
            SecondName = secondName;
            Email = email;
            Phone = phone;
        }
    }
}
