using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using FarmApp.Data;
using Microsoft.AspNetCore.Identity;
using FarmApp.Models;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using FarmApp.Models.Services;

namespace FarmApp
{
    /// <summary>
    /// Main class for user interface.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="configuration">Program configuration.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Configuration service.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Method for configuration services.
        /// </summary>
        /// <param name="services">Services.</param>
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddScoped<IFavouriteService, FavouriteService>();
            services.AddScoped<IShopService, ShopService>();

            services.AddAuthorization(options => 
            {
                options.AddPolicy("Farmers", policy => 
                {
                    policy.RequireAuthenticatedUser()
                    .RequireRole("Farmer");
                });

                options.AddPolicy("Customers", policy =>
                {
                    policy.RequireAuthenticatedUser()
                    .RequireRole("Customer");
                });
            });

            services.AddRazorPages()
                .AddRazorRuntimeCompilation()
                .AddRazorPagesOptions(options =>
                {
                    options.Conventions.AuthorizeFolder("/Authorized");
                    options.Conventions.AuthorizeFolder("/Authorized/Farmer", "Farmers");
                    options.Conventions.AuthorizeFolder("/Authorized/Customer", "Customers");
                });

            services.AddControllers();

            services.AddDbContext<FarmAppContext>(options =>
                    options.UseSqlite(Configuration.GetConnectionString("FarmAppContext")));

            services.AddIdentity<User, IdentityRole>(options => {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<FarmAppContext>()
                .AddDefaultTokenProviders();
            
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = $"/Identity/Account/Login";
                options.LogoutPath = $"/Identity/Account/Logout";
                options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
            });
        }
        
        /// <summary>
        /// Method for configuration application environment.
        /// </summary>
        /// <param name="app">Application builder.</param>
        /// <param name="env">Application Environment.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            var defaultCulture = new CultureInfo("en-US");
            var localizationOptions = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(defaultCulture),
                SupportedCultures = new List<CultureInfo> { defaultCulture },
                SupportedUICultures = new List<CultureInfo> { defaultCulture }
            };
            app.UseRequestLocalization(localizationOptions);

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });
        }
    }
}
