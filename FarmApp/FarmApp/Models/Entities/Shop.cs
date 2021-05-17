using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FarmApp.Models
{
    public class Shop : BaseEntity
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
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:0.0000000}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "decimal(10, 7)")]
        [Range(-90.0, 90.0, ErrorMessage = "The value {0} must be between {1} and {2}.")]
        public decimal Latitude { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:0.0000000}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "decimal(10, 7)")]
        [Range(-180.0, 180.0, ErrorMessage = "The value {0} must be between {1} and {2}.")]
        public decimal Longitude { get; set; }

        public byte[] Image { get; set; }

        //[Required]
        public User Owner { get; set; }

        public List<Review> Reviews { get; set; }

        public List<Favourite> Favourites { get; set; }

        public List<Message> Messages { get; set; }

    }
}
