﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FarmApp.Models
{
    public class Shop
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [DataType(DataType.Text)]
        public string Description { get; set; }

        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Street")]
        public string Street { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "City")]
        public string City { get; set; }

        [DataType(DataType.PostalCode)]
        [Display(Name = "Postal Code")]
        public int PostalCode { get; set; }

        [Required]
        [Range(-90.0, 90.0, ErrorMessage = "The value {0} must be between {1} and {2}.")]
        public double Latitude { get; set; }

        [Required]
        [Range(-180.0, 180.0, ErrorMessage = "The value {0} must be between {1} and {2}.")]
        public double Longitude { get; set; }

        [Required]
        public User Owner { get; set; }

        public List<Review> Reviews { get; set; }

        public List<Favourite> Favourites { get; set; }

        public List<Message> Messages { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreateDate { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime ModifiedDate { get; set; }

    }
}
