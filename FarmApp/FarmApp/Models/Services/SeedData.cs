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

        // Init data
        // Admin
        private const string AdminEmail = "admin@farmapp.com";
        private const string AdminPassword = "admin123.";

        // Customer
        private const string CustomerEmail = "customer@farmapp.com";
        private const string CustomerPassword = "customer123.";

        // Farmer
        private const string FarmerEmail = "farmer@farmapp.com";
        private const string FarmerPassword = "farmer123.";
        private const string FarmerFirstName = "Jan";
        private const string FarmerLastName = "Farmář";
        private const string FarmerStreet = "Klatovská 23";
        private const string FarmerCity = "Plzeň";
        private const int FarmerPostalCode = 30100;


        public static void Initialize(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            using (var context = new FarmAppContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<FarmAppContext>>()))
            {

                // TAKY FUNGUJE
                //var adminExists = roleManager.RoleExistsAsync(ERole.ADMIN);
                //adminExists.Wait();

                //if (!adminExists.Result)
                //{
                //    context.Roles.Add(
                //        new IdentityRole
                //        {
                //            Name = ERole.ADMIN,
                //            NormalizedName = ERole.ADMIN.ToUpper()
                //        }
                //    );
                //    context.SaveChanges();
                //}

                if (!context.Roles.Any())
                {
                    context.Roles.AddRange(
                        new IdentityRole
                        {
                            Name = ERole.ADMIN,
                            NormalizedName = ERole.ADMIN.ToUpper()
                        },

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

                if (!context.Users.Any())
                {
                    var userAdmin = new User
                    {
                        UserName = AdminEmail,
                        Email = AdminEmail,
                        IsFarmer = 0
                    };

                    var userCustomer = new User
                    {
                        UserName = CustomerEmail,
                        Email = CustomerEmail,
                        IsFarmer = 0
                    };

                    var userFarmer = new User
                    {
                        UserName = FarmerEmail,
                        Email = FarmerEmail,
                        FirstName = FarmerFirstName,
                        LastName = FarmerLastName,
                        Street = FarmerStreet,
                        City = FarmerCity,
                        PostalCode = FarmerPostalCode,
                        IsFarmer = 1
                    };

                    userManager.CreateAsync(userAdmin, AdminPassword);
                    userManager.CreateAsync(userCustomer, CustomerPassword);
                    userManager.CreateAsync(userFarmer, FarmerPassword);

                    // Add users to roles
                    userManager.AddToRoleAsync(userAdmin, ERole.ADMIN);
                    userManager.AddToRoleAsync(userCustomer, ERole.CUSTOMER);
                    userManager.AddToRoleAsync(userFarmer, ERole.FARMER);

                }
            }
        }
    }
}
