using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FarmApp.Models
{
    /// <summary>
    /// Model for shop.
    /// </summary>
    public class Shop : BaseEntity
    {
        /// <summary>
        /// Identificator.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Shop name.
        /// </summary>
        [Required]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        /// <summary>
        /// Shop description.
        /// </summary>
        [DataType(DataType.Text)]
        public string Description { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        /// <summary>
        /// Email address.
        /// </summary>
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Address")]
        public string Address { get; set; }

        /// <summary>
        /// Shop latitude.
        /// </summary>
        [Required]
        [DisplayFormat(DataFormatString = "{0:0.0000000}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "decimal(10, 7)")]
        [Range(-90.0, 90.0, ErrorMessage = "The value {0} must be between {1} and {2}.")]
        public decimal Latitude { get; set; }

        /// <summary>
        /// Shop longitude.
        /// </summary>
        [Required]
        [DisplayFormat(DataFormatString = "{0:0.0000000}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "decimal(10, 7)")]
        [Range(-180.0, 180.0, ErrorMessage = "The value {0} must be between {1} and {2}.")]
        public decimal Longitude { get; set; }

        /// <summary>
        /// Thumbnail of shop.
        /// </summary>
        public byte[] Image { get; set; }

        /// <summary>
        /// Shop owner.
        /// </summary>
        public User Owner { get; set; }

        /// <summary>
        /// Reviews for shop.
        /// </summary>
        public List<Review> Reviews { get; set; }

        /// <summary>
        /// List of favourites.
        /// </summary>
        public List<Favourite> Favourites { get; set; }

        /// <summary>
        /// List of messages.
        /// </summary>
        public List<Message> Messages { get; set; }
    }
}
