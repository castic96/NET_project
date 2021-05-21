namespace FarmApp.Models.DTO
{
    /// <summary>
    /// Structure that includes data about shop in structure for transfer.
    /// </summary>
    public class ShopDTO
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id">Shop id.</param>
        /// <param name="name">Shop name.</param>
        /// <param name="description">Shop description.</param>
        /// <param name="address">Shop address.</param>
        /// <param name="latitude">Shop latitude.</param>
        /// <param name="longitude">Shop longitude.</param>
        public ShopDTO(int id, string name, string description, string address, decimal latitude, decimal longitude)
        {
            Id = id;
            Name = name;
            Description = description;
            Address = address;
            Latitude = latitude;
            Longitude = longitude;
        }

        /// <summary>
        /// Shop id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Shop name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Shop description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Shop address.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Shop latitude.
        /// </summary>
        public decimal Latitude { get; set; }

        /// <summary>
        /// Shop longitude.
        /// </summary>
        public decimal Longitude { get; set; }

        /// <summary>
        /// Pretty print all of arguments.
        /// </summary>
        /// <returns>Information about shop as string.</returns>
        public override string ToString()
        {
            return "{\"id\": \"" + Id + "\"," +
                "\"name\": \"" + Name + "\", " +
                "\"description\": \"" + Description + "\", " +
                "\"address\": \"" + Address + "\", " +
                "\"latitude\": \"" + Latitude + "\", " +
                "\"longitude\": \"" + Longitude + "\"}";
        }

    }
}
