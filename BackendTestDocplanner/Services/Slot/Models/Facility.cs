namespace BackendTestDocplanner.Services.Slot.Models
{
    /// <summary>
    /// Defines a facility as provided in the slot service
    /// </summary>
    public class Facility
    {
        /// <summary>
        /// Facility name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Facility address
        /// </summary>
        public string Address { get; set; }

        public Facility(string name, string address)
        {
            Name = name;
            Address = address;
        }
    }
}
