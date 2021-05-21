using System.ComponentModel.DataAnnotations;

namespace FarmApp.Models
{
    /// <summary>
    /// Model for message.
    /// </summary>
    public class Message : BaseEntity
    {
        /// <summary>
        /// Identificator.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// User.
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Shop.
        /// </summary>
        public Shop Shop { get; set; }

        /// <summary>
        /// Flag if the message was written by user.
        /// </summary>
        [Required]
        public bool FromUser { get; set; }

        /// <summary>
        /// Message content.
        /// </summary>
        [Required]
        public string Content { get; set; }
    }
}
