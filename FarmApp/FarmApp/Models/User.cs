using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FarmApp.Models
{
    public class User : IdentityUser
    {
        [PersonalData]
        [Display(Name = "First Name")]
        [Column(TypeName = "nvarchar(100)")]
        public string FirstName { get; set; }

        [PersonalData]
        [Display(Name = "Last Name")]
        [Column(TypeName = "nvarchar(100)")]
        public string LastName { get; set; }
    }
}
