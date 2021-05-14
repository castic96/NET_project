using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FarmApp.Models.DTO
{
    public class ShopDTO
    {
        public ShopDTO(int id, string name, string description, string street, string city, int postalCode, decimal latitude, decimal longitude)
        {
            Id = id;
            Name = name;
            Description = description;
            Street = street;
            City = city;
            PostalCode = postalCode;
            Latitude = latitude;
            Longitude = longitude;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public int PostalCode { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

        public override string ToString()
        {
            return "{\"id\": \"" + Id + "\"," +
                "\"name\": \"" + Name + "\", " +
                "\"description\": \"" + Description + "\", " +
                "\"street\": \"" + Street + "\", " +
                "\"city\": \"" + City + "\", " +
                "\"postalcode\": \"" + PostalCode + "\", " +
                "\"latitude\": \"" + Latitude + "\", " +
                "\"longitude\": \"" + Longitude + "\"}";
        }

    }
}
