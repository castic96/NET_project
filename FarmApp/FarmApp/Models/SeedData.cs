using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using FarmApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace FarmApp.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new FarmAppContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<FarmAppContext>>()))
            {
                // Look for any movies.
                if (!context.Movie.Any())
                {

                    context.Movie.AddRange(
                        new Movie
                        {
                            Title = "When Harry Met Sally",
                            ReleaseDate = DateTime.Parse("1989-2-12"),
                            Genre = "Romantic Comedy",
                            Price = 7.99M
                        },

                        new Movie
                        {
                            Title = "Ghostbusters ",
                            ReleaseDate = DateTime.Parse("1984-3-13"),
                            Genre = "Comedy",
                            Price = 8.99M
                        },

                        new Movie
                        {
                            Title = "Ghostbusters 2",
                            ReleaseDate = DateTime.Parse("1986-2-23"),
                            Genre = "Comedy",
                            Price = 9.99M
                        },

                        new Movie
                        {
                            Title = "Rio Bravo",
                            ReleaseDate = DateTime.Parse("1959-4-15"),
                            Genre = "Western",
                            Price = 3.99M
                        }
                    );
                    context.SaveChanges();
                }

                if (!context.Roles.Any())
                {
                    context.Roles.AddRange(
                        new IdentityRole
                        {
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },

                        new IdentityRole
                        {
                            Name = "Customer",
                            NormalizedName = "CUSTOMER"
                        },

                        new IdentityRole
                        {
                            Name = "Farmer",
                            NormalizedName = "FARMER"
                        }
                    );
                    context.SaveChanges();
                }
            }
        }
    }
}
