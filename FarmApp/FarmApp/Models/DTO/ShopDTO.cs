using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FarmApp.Models.DTO
{
    public class ShopDTO
    {
        public ShopDTO(int id, string name, string description, string address, decimal latitude, decimal longitude)
        {
            Id = id;
            Name = name;
            Description = description;
            Address = address;
            Latitude = latitude;
            Longitude = longitude;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

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
