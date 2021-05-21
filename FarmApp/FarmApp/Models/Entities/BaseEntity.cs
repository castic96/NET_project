using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace FarmApp.Models
{
    /// <summary>
    /// Base entity - includes timestamps.
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// Date of creation the entity.
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Date of last modification the entity.
        /// </summary>
        public DateTime ModifiedDate { get; set; }
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public BaseEntity()
        {
            CreateDate = DateTime.UtcNow;
            ModifiedDate = DateTime.UtcNow;
        }

    }
}
