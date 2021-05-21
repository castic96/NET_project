using Microsoft.EntityFrameworkCore;
using FarmApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace FarmApp.Data
{
    /// <summary>
    /// Class for initialization of database context.
    /// </summary>
    public class FarmAppContext : IdentityDbContext<User, IdentityRole, string>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="options">Options.</param>
        public FarmAppContext (DbContextOptions<FarmAppContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Shops.
        /// </summary>
        public DbSet<Shop> Shops { get; set; }

        /// <summary>
        /// Reviews.
        /// </summary>
        public DbSet<Review> Reviews { get; set; }

        /// <summary>
        /// Messages.
        /// </summary>
        public DbSet<Message> Messages { get; set; }

        /// <summary>
        /// Favourites.
        /// </summary>
        public DbSet<Favourite> Favourites { get; set; }
    }
}
