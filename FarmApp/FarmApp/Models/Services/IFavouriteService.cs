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
    /// Interface for FavouriteService.
    /// </summary>
    public interface IFavouriteService
    {
        /// <summary>
        /// Finds out if current shop is favourite for current user.
        /// </summary>
        /// <param name="shop">Current shop</param>
        /// <param name="userId">Id of current user.</param>
        /// <returns>true if current shop is favourite for current user, false otherwise.</returns>
        public bool IsFavourite(Shop shop, string userId);

        /// <summary>
        /// Finds id of favourite shop.
        /// </summary>
        /// <param name="shop">Current shop.</param>
        /// <param name="userId">Id of current user.</param>
        /// <returns>Id of favourite shop.</returns>
        public int FindFavouriteId(Shop shop, string userId);
    }
}
