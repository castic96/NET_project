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
    public class FavouriteService : IFavouriteService
    {

        private readonly FarmApp.Data.FarmAppContext _context;
        private readonly UserManager<User> _userManager;

        public FavouriteService(FarmApp.Data.FarmAppContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public bool IsFavourite(Shop shop, string userId)
        {
            var favourites = _context.Favourites.FirstOrDefault(f => f.User.Id == userId && f.Shop.Id == shop.Id);

            if (favourites == null) return false;
            
            return true;
        }

        public int FindFavouriteId(Shop shop, string userId)
        {
            var favourites = _context.Favourites.FirstOrDefault(f => f.User.Id == userId && f.Shop.Id == shop.Id);

            if (favourites == null) return -1;

            return favourites.Id;
        }

    }
}
