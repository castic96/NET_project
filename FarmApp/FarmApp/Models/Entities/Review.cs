using System;
using System.ComponentModel.DataAnnotations;

namespace FarmApp.Models
{
    /// <summary>
    /// Model for review.
    /// </summary>
    public class Review : BaseEntity
    {
        /// <summary>
        /// Identificator.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Author of review.
        /// </summary>
        public User Author { get; set; }

        /// <summary>
        /// To shop.
        /// </summary>
        public Shop Shop { get; set; }

        /// <summary>
        /// Rating.
        /// </summary>
        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        /// <summary>
        /// Comment.
        /// </summary>
        [DataType(DataType.Text)]
        public string Comment { get; set; }
    }
}
