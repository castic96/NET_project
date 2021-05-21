using System;
using FarmApp.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(FarmApp.Areas.Identity.IdentityHostingStartup))]
namespace FarmApp.Areas.Identity
{
    /// <summary>
    /// Startup class for Identity.
    /// </summary>
    public class IdentityHostingStartup : IHostingStartup
    {
        /// <summary>
        /// Method for configuration Identity.
        /// </summary>
        /// <param name="builder">Builder.</param>
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}