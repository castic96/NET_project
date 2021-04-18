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
    public interface IFavouriteService
    {
        public bool IsFavourite(Shop shop, string userId);
        public int FindFavouriteId(Shop shop, string userId);

    }
}
