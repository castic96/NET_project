using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using FarmApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace FarmApp.Models.Services
{
    public static class SeedData
    {

        // --- Init data ----
        // ------------------
        //
        // --- Users ----
        // Admin
        private const string AdminEmail = "admin@farmapp.com";
        private const string AdminPassword = "admin123.";

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

        // --- Shops ---
        // Shop1
        private const int IdShop1 = 1;
        private const string NameShop1 = "Shop One";
        private const string DescriptionShop1 = "Integer maximus orci id quam dignissim, eget suscipit arcu ullamcorper. Suspendisse aliquet lacus vel ligula hendrerit luctus. Donec iaculis convallis semper. Vestibulum ultrices eros sed consequat pretium. Sed sollicitudin lobortis sapien id commodo. Integer placerat sodales odio a bibendum. Nunc faucibus scelerisque luctus.";
        private const string EmailShop1 = "shopone@shopone.com";
        private const string StreetShop1 = "Ke Křižovatce 412";
        private const string CityShop1 = "Praha";
        private const int PostalCodeShop1 = 213334;
        private const decimal LatitudeShop1 = 89.33M;
        private const decimal LongitudeShop1 = 59.214M;

        // Shop2
        private const int IdShop2 = 2;
        private const string NameShop2 = "Shop Two";
        private const string DescriptionShop2 = "Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae; Nunc ultricies venenatis lorem sit amet accumsan. Vivamus aliquet at felis sed bibendum. Interdum et malesuada fames ac ante ipsum primis in faucibus. Integer maximus quam dui, id interdum dolor efficitur a. Praesent tincidunt eu ante eu pulvinar.";
        private const string EmailShop2 = "shoptwo@shoptwo.com";
        private const string StreetShop2 = "Chebská 234";
        private const string CityShop2 = "Ostrava";
        private const int PostalCodeShop2 = 12345;
        private const decimal LatitudeShop2 = 12.998M;
        private const decimal LongitudeShop2 = 0.5513M;

        // Shop3
        private const int IdShop3 = 3;
        private const string NameShop3 = "Shop Three";
        private const string DescriptionShop3 = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi purus dolor, pharetra et volutpat mattis, faucibus vitae tellus. Curabitur imperdiet semper tellus, id egestas lorem. In justo ipsum, consequat at massa non, tincidunt varius urna. Nullam eu lectus accumsan lacus efficitur blandit. Nam sodales metus orci. Nullam tempor velit commodo justo feugiat, non lacinia dui varius. Nam accumsan nibh sit amet laoreet tincidunt.";
        private const string EmailShop3 = "shopthree@shopthree.com";
        private const string StreetShop3 = "Technická 9";
        private const string CityShop3 = "Plzeň";
        private const int PostalCodeShop3 = 30100;
        private const decimal LatitudeShop3 = 2.33M;
        private const decimal LongitudeShop3 = 100.214M;

        // --- Reviews ---
        // Review1
        private const int IdReview1 = 1;
        private const int RatingReview1 = 3;
        private const string CommentReview1 = "Phasellus maximus mi id iaculis sodales. Donec blandit ex eget erat lobortis maximus. Fusce elementum lacinia eros, vitae tincidunt nunc aliquet et. Pellentesque eu rutrum enim. Morbi consectetur risus in urna mattis dapibus a et nisi. Vestibulum mattis libero in fringilla hendrerit. Sed eleifend in nunc ac tincidunt. Vivamus tincidunt tincidunt neque, ut ullamcorper nisl. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus.";

        // Review2
        private const int IdReview2 = 2;
        private const int RatingReview2 = 1;
        private const string CommentReview2 = "Praesent tincidunt eu ante eu pulvinar. Proin tincidunt, lorem sagittis porttitor aliquam, elit justo varius neque, vel laoreet urna tortor ut neque. Donec et est turpis.";

        // Review3
        private const int IdReview3 = 3;
        private const int RatingReview3 = 4;
        private const string CommentReview3 = "Cras fringilla et diam quis iaculis. Vivamus dictum tristique nisi eu convallis. Donec condimentum ligula dui, in molestie enim tincidunt nec. Nunc scelerisque tincidunt erat, ac pretium magna auctor at.";


        public static void Initialize(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            InitializeRoles(serviceProvider);
            InitializeUsers(serviceProvider);
            InitializeShops(serviceProvider);
            InitializeReviews(serviceProvider);
            InitializeFavourites(serviceProvider);
        }

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
            }
        }

        public static void InitializeUsers(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            using (var context = new FarmAppContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<FarmAppContext>>()))
            {
                if (!context.Users.Any())
                {
                    var userAdmin = new User
                    {
                        UserName = AdminEmail,
                        Email = AdminEmail,
                        IsFarmer = 0
                    };

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

                    userManager.CreateAsync(userAdmin, AdminPassword);
                    userManager.CreateAsync(userCustomer1, Customer1Password);
                    userManager.CreateAsync(userCustomer2, Customer2Password);
                    userManager.CreateAsync(userFarmer1, Farmer1Password);
                    userManager.CreateAsync(userFarmer2, Farmer2Password);

                    // Add users to roles
                    userManager.AddToRoleAsync(userAdmin, ERole.ADMIN);
                    userManager.AddToRoleAsync(userCustomer1, ERole.CUSTOMER);
                    userManager.AddToRoleAsync(userCustomer2, ERole.CUSTOMER);
                    userManager.AddToRoleAsync(userFarmer1, ERole.FARMER);
                    userManager.AddToRoleAsync(userFarmer2, ERole.FARMER);
                }
            }

        }

        public static void InitializeShops(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            using (var context = new FarmAppContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<FarmAppContext>>()))
            {
                if (!context.Shops.Any())
                {

                    var shop1 = new Shop
                    {
                        Name = NameShop1,
                        Description = DescriptionShop1,
                        Email = EmailShop1,
                        Address = StreetShop1 + " " + PostalCodeShop1 + ", " + CityShop1,
                        Latitude = LatitudeShop1,
                        Longitude = LongitudeShop1,
                        Owner = (context.Users.FirstOrDefault(user => user.UserName == Farmer1Email))
                    };

                    var shop2 = new Shop
                    {
                        Name = NameShop2,
                        Description = DescriptionShop2,
                        Email = EmailShop2,
                        Address = StreetShop2 + " " + PostalCodeShop2 + ", " + CityShop2,
                        Latitude = LatitudeShop2,
                        Longitude = LongitudeShop2,
                        Owner = (context.Users.FirstOrDefault(user => user.UserName == Farmer1Email))
                    };

                    var shop3 = new Shop
                    {
                        Name = NameShop3,
                        Description = DescriptionShop3,
                        Email = EmailShop3,
                        Address = StreetShop3 + " " + PostalCodeShop3 + ", " + CityShop3,
                        Latitude = LatitudeShop3,
                        Longitude = LongitudeShop3,
                        Owner = (context.Users.FirstOrDefault(user => user.UserName == Farmer2Email))
                    };

                    context.Shops.AddRange(shop1, shop2, shop3);
                    context.SaveChanges();
                }
            }
        }

        public static void InitializeReviews(IServiceProvider serviceProvider)
        {
            using (var context = new FarmAppContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<FarmAppContext>>()))
            {
                if (!context.Reviews.Any())
                {
                    var review1 = new Review
                    {
                        Rating = RatingReview1,
                        Comment = CommentReview1,
                        Author = (context.Users.FirstOrDefault(user => user.UserName == Customer1Email)),
                        Shop = (context.Shops.FirstOrDefault(shop => shop.Id == IdShop1))
                    };

                    var review2 = new Review
                    {
                        Rating = RatingReview2,
                        Comment = CommentReview2,
                        Author = (context.Users.FirstOrDefault(user => user.UserName == Customer1Email)),
                        Shop = (context.Shops.FirstOrDefault(shop => shop.Id == IdShop2))
                    };

                    var review3 = new Review
                    {
                        Rating = RatingReview3,
                        Comment = CommentReview3,
                        Author = (context.Users.FirstOrDefault(user => user.UserName == Customer2Email)),
                        Shop = (context.Shops.FirstOrDefault(shop => shop.Id == IdShop1))
                    };

                    context.Reviews.AddRange(review1, review2, review3);
                    context.SaveChanges();
                }
            }

        }

        public static void InitializeFavourites(IServiceProvider serviceProvider)
        {
            using (var context = new FarmAppContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<FarmAppContext>>()))
            {
                if (!context.Favourites.Any())
                {
                    var favourite1 = new Favourite
                    {
                        User = (context.Users.FirstOrDefault(user => user.UserName == Customer1Email)),
                        Shop = (context.Shops.FirstOrDefault(shop => shop.Id == IdShop3))
                    };

                    var favourite2 = new Favourite
                    {
                        User = (context.Users.FirstOrDefault(user => user.UserName == Customer2Email)),
                        Shop = (context.Shops.FirstOrDefault(shop => shop.Id == IdShop1))
                    };

                    context.Favourites.AddRange(favourite1, favourite2);
                    context.SaveChanges();
                }
            }

        }
    }
}
