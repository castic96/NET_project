using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FarmApp.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public User Author { get; set; }

        [Required]
        public Shop Shop { get; set; }

        [Required]
        public int Rating { get; set; }

        [DataType(DataType.Text)]
        public string Comment { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreateDate { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime ModifiedDate { get; set; }
    }
}
