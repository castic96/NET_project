using System.ComponentModel.DataAnnotations;

namespace FarmApp.Models
{
    /// <summary>
    /// Model for favourite.
    /// </summary>
    public class Favourite : BaseEntity
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
    }
}
