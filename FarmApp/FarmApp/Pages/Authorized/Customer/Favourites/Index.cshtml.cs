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

namespace FarmApp.Pages.Authorized.Customer.Favourites
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

        public IList<Favourite> Favourite { get;set; }

        public Dictionary<int, int> RatingAverages { get; set; }

        public async Task OnGetAsync()
        {
            Favourite = await _context.Favourites
                                    .Where(f => f.User.Id == _userManager.GetUserId(User))
                                    .Include(f => f.Shop)
                                    .Include(f => f.Shop.Reviews)
                                    .OrderByDescending(f => f.CreateDate)
                                    .ToListAsync();

            calculateRatingAverages();

        }

        private void calculateRatingAverages()
        {
            RatingAverages = new Dictionary<int, int>();

            foreach (var currentFavourite in Favourite)
            {
                if (currentFavourite.Shop.Reviews == null || currentFavourite.Shop.Reviews.Count <= 0)
                {
                    RatingAverages.Add(currentFavourite.Shop.Id, 0);
                }
                else
                {
                    int sum = 0;

                    foreach (var currentReview in currentFavourite.Shop.Reviews)
                    {
                        sum += currentReview.Rating;
                    }

                    RatingAverages.Add(currentFavourite.Shop.Id, (int)((sum / (currentFavourite.Shop.Reviews.Count * 5.0)) * 100));
                }
            }
        }
    }
}
