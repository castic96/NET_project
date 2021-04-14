using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FarmApp.Models
{
    public class BaseEntity
    {
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        
        public BaseEntity()
        {
            CreateDate = DateTime.UtcNow;
            ModifiedDate = DateTime.UtcNow;

        }

    }
}
