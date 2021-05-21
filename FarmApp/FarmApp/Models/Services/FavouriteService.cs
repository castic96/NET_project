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
    /// <summary>
    /// Service for Favourites.
    /// </summary>
    public class FavouriteService : IFavouriteService
    {
        /// <summary>
        /// Database context.
        /// </summary>
        private readonly FarmAppContext _context;

        /// <summary>
        /// User manager.
        /// </summary>
        private readonly UserManager<User> _userManager;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="context">Database context.</param>
        /// <param name="userManager">User manager.</param>
        public FavouriteService(FarmAppContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        /// <summary>
        /// Finds out if current shop is favourite for current user.
        /// </summary>
        /// <param name="shop">Current shop</param>
        /// <param name="userId">Id of current user.</param>
        /// <returns>true if current shop is favourite for current user, false otherwise.</returns>
        public bool IsFavourite(Shop shop, string userId)
        {
            var favourites = _context.Favourites.FirstOrDefault(f => f.User.Id == userId && f.Shop.Id == shop.Id);

            if (favourites == null) return false;
            
            return true;
        }

        /// <summary>
        /// Finds id of favourite shop.
        /// </summary>
        /// <param name="shop">Current shop.</param>
        /// <param name="userId">Id of current user.</param>
        /// <returns>Id of favourite shop.</returns>
        public int FindFavouriteId(Shop shop, string userId)
        {
            var favourites = _context.Favourites.FirstOrDefault(f => f.User.Id == userId && f.Shop.Id == shop.Id);

            if (favourites == null) return -1;

            return favourites.Id;
        }

    }
}
