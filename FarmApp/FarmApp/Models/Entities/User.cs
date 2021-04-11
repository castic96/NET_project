using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FarmApp.Models
{
    public class User : IdentityUser
    {
        [PersonalData]
        [Display(Name = "First Name")]
        [Column(TypeName = "nvarchar(100)")]
        public string FirstName { get; set; }

        [PersonalData]
        [Display(Name = "Last Name")]
        [Column(TypeName = "nvarchar(100)")]
        public string LastName { get; set; }

        [PersonalData]
        [Display(Name = "Street")]
        [Column(TypeName = "nvarchar(100)")]
        public string Street { get; set; }

        [PersonalData]
        [Display(Name = "City")]
        [Column(TypeName = "nvarchar(100)")]
        public string City { get; set; }

        [PersonalData]
        [Display(Name = "Postal Code")]
        [Column(TypeName = "bigint")]
        public int PostalCode { get; set; }

        [Column(TypeName = "tinyint")]
        public int IsFarmer { get; set; }

        public List<Shop> Shops { get; set; }

        public List<Review> Reviews { get; set; }

        public List<Favourite> Favourites { get; set; }

        public List<Message> Messages { get; set; }
    }
}
