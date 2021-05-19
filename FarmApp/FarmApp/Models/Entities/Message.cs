using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FarmApp.Models
{
    public class Message : BaseEntity
    {

        [Key]
        public int Id { get; set; }

        public User User { get; set; }

        public Shop Shop { get; set; }

        [Required]
        public bool FromUser { get; set; }

        [Required]
        public string Content { get; set; }

    }
}
