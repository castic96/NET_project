using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FarmApp.Models
{
    /// <summary>
    /// Model for user.
    /// </summary>
    public class User : IdentityUser
    {
        /// <summary>
        /// First name.
        /// </summary>
        [PersonalData]
        [Display(Name = "First Name")]
        [Column(TypeName = "nvarchar(100)")]
        public string FirstName { get; set; }

        /// <summary>
        /// Last name.
        /// </summary>
        [PersonalData]
        [Display(Name = "Last Name")]
        [Column(TypeName = "nvarchar(100)")]
        public string LastName { get; set; }

        /// <summary>
        /// Street.
        /// </summary>
        [PersonalData]
        [Display(Name = "Street")]
        [Column(TypeName = "nvarchar(100)")]
        public string Street { get; set; }

        /// <summary>
        /// City.
        /// </summary>
        [PersonalData]
        [Display(Name = "City")]
        [Column(TypeName = "nvarchar(100)")]
        public string City { get; set; }

        /// <summary>
        /// Postal code.
        /// </summary>
        [PersonalData]
        [Display(Name = "Postal Code")]
        [Column(TypeName = "bigint")]
        public int PostalCode { get; set; }

        /// <summary>
        /// Flag if the user is farmer or not.
        /// </summary>
        [Column(TypeName = "tinyint")]
        public int IsFarmer { get; set; }

        /// <summary>
        /// List of owned shops.
        /// </summary>
        public List<Shop> Shops { get; set; }

        /// <summary>
        /// List of written reviews.
        /// </summary>
        public List<Review> Reviews { get; set; }

        /// <summary>
        /// List of favourite shops.
        /// </summary>
        public List<Favourite> Favourites { get; set; }

        /// <summary>
        /// List of sent and received messages.
        /// </summary>
        public List<Message> Messages { get; set; }
    }
}
