using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FarmApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace FarmApp.Data
{
    public class FarmAppContext : IdentityDbContext<User, IdentityRole, string>
    {
        public FarmAppContext (DbContextOptions<FarmAppContext> options)
            : base(options)
        {
        }

        public DbSet<FarmApp.Models.Movie> Movie { get; set; }
    }
}
