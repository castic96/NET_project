using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using FarmApp.Data;
using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace FarmApp.Models.Services
{
    /// <summary>
    /// Seed data class creates initialize data.
    /// </summary>
    public static class SeedData
    {
        // --- Users ----

        // Customer1
        private const string Customer1Email = "customer1@farmapp.com";
        private const string Customer1Password = "customer123.";

        // Customer2
        private const string Customer2Email = "customer2@farmapp.com";
        private const string Customer2Password = "customer123.";

        // Farmer1
        private const string Farmer1Email = "farmer1@farmapp.com";
        private const string Farmer1Password = "farmer123.";
        private const string Farmer1FirstName = "Jan";
        private const string Farmer1LastName = "Farmář";
        private const string Farmer1Street = "Klatovská 23";
        private const string Farmer1City = "Plzeň";
        private const int Farmer1PostalCode = 30100;

        // Farmer2
        private const string Farmer2Email = "farmer2@farmapp.com";
        private const string Farmer2Password = "farmer123.";
        private const string Farmer2FirstName = "Petr";
        private const string Farmer2LastName = "Nový";
        private const string Farmer2Street = "Přemyslovská 89";
        private const string Farmer2City = "Praha";
        private const int Farmer2PostalCode = 23080;

        /// <summary>
        /// Runs all submethods for data initialization.
        /// </summary>
        /// <param name="serviceProvider">Service provider.</param>
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            InitializeRoles(serviceProvider);
            InitializeUsers(serviceProvider);
        }

        /// <summary>
        /// Initializes roles, if do not exist.
        /// </summary>
        /// <param name="serviceProvider">Service provider.</param>
        public static void InitializeRoles(IServiceProvider serviceProvider)
        {
            using (var context = new FarmAppContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<FarmAppContext>>()))
            {
                if (!context.Roles.Any())
                {
                    context.Roles.AddRange(

                        new IdentityRole
                        {
                            Name = ERole.CUSTOMER,
                            NormalizedName = ERole.CUSTOMER.ToUpper()
                        },

                        new IdentityRole
                        {
                            Name = ERole.FARMER,
                            NormalizedName = ERole.FARMER.ToUpper()
                        }
                    );
                    context.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Initializes users, if do not exist.
        /// </summary>
        /// <param name="serviceProvider">Service provider.</param>
        public static void InitializeUsers(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            using (var context = new FarmAppContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<FarmAppContext>>()))
            {
                if (!context.Users.Any())
                {

                    var userCustomer1 = new User
                    {
                        UserName = Customer1Email,
                        Email = Customer1Email,
                        IsFarmer = 0
                    };

                    var userCustomer2 = new User
                    {
                        UserName = Customer2Email,
                        Email = Customer2Email,
                        IsFarmer = 0
                    };

                    var userFarmer1 = new User
                    {
                        UserName = Farmer1Email,
                        Email = Farmer1Email,
                        FirstName = Farmer1FirstName,
                        LastName = Farmer1LastName,
                        Street = Farmer1Street,
                        City = Farmer1City,
                        PostalCode = Farmer1PostalCode,
                        IsFarmer = 1
                    };

                    var userFarmer2 = new User
                    {
                        UserName = Farmer2Email,
                        Email = Farmer2Email,
                        FirstName = Farmer2FirstName,
                        LastName = Farmer2LastName,
                        Street = Farmer2Street,
                        City = Farmer2City,
                        PostalCode = Farmer2PostalCode,
                        IsFarmer = 1
                    };

                    userManager.CreateAsync(userCustomer1, Customer1Password);
                    userManager.CreateAsync(userCustomer2, Customer2Password);
                    userManager.CreateAsync(userFarmer1, Farmer1Password);
                    userManager.CreateAsync(userFarmer2, Farmer2Password);

                    // Add users to roles
                    userManager.AddToRoleAsync(userCustomer1, ERole.CUSTOMER);
                    userManager.AddToRoleAsync(userCustomer2, ERole.CUSTOMER);
                    userManager.AddToRoleAsync(userFarmer1, ERole.FARMER);
                    userManager.AddToRoleAsync(userFarmer2, ERole.FARMER);
                }
            }

        }

    }
}
