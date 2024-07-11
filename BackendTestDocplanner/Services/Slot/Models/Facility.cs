namespace BackendTestDocplanner.Services.Slot.Models
{
    /// <summary>
    /// Defines a facility as provided in the slot service
    /// </summary>
    public class Facility
    {
        /// <summary>
        /// Facility id
        /// </summary>
        public string FacilityId { get; set; }

        /// <summary>
        /// Facility name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Facility address
        /// </summary>
        public string Address { get; set; }

        public Facility(string facilityId, string name, string address)
        {
            FacilityId = facilityId;
            Name = name;
            Address = address;
        }
    }
}
