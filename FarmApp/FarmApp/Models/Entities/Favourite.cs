using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FarmApp.Models
{
    public class Favourite : BaseEntity
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public User User { get; set; }

        [Required]
        public Shop Shop { get; set; }

    }
}
