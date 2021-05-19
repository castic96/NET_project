using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FarmApp.Data;
using FarmApp.Models;
using Microsoft.AspNetCore.Identity;

namespace FarmApp.Pages.Authorized.Farmer.Shops
{
    public class IndexModel : PageModel
    {
        private readonly FarmApp.Data.FarmAppContext _context;
        private readonly UserManager<User> _userManager;

        public IndexModel(FarmApp.Data.FarmAppContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<Shop> Shop { get;set; }

        public Dictionary<int, int> RatingAverages { get; set; }

        public async Task OnGetAsync()
        {
            Shop = await _context.Shops
                                    .Where(shop => shop.Owner.Id == _userManager.GetUserId(User))
                                    .Include(shop => shop.Reviews)
                                    .OrderByDescending(shop => shop.CreateDate)
                                    .ToListAsync();

            calculateRatingAverages();

        }

        private void calculateRatingAverages()
        {
            RatingAverages = new Dictionary<int, int>();

            foreach (var currentShop in Shop)
            {
                if (currentShop.Reviews == null || currentShop.Reviews.Count <= 0)
                {
                    RatingAverages.Add(currentShop.Id, 0);
                }
                else
                {
                    int sum = 0;

                    foreach (var currentReview in currentShop.Reviews)
                    {
                        sum += currentReview.Rating;
                    }

                    RatingAverages.Add(currentShop.Id, (int)((sum / (currentShop.Reviews.Count * 5.0)) * 100));
                }
            }
        }
    }
}
