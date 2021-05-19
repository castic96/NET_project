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

namespace FarmApp.Pages.Authorized.Customer.Find
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

        [BindProperty]
        public string Text { get; set; }

        public IList<Shop> Shop { get;set; }

        public Dictionary<int, int> RatingAverages { get; set; }

        public async Task OnGetAsync()
        {
            Shop = await _context.Shops
                                    .OrderByDescending(shop => shop.CreateDate)
                                    .Include(shop => shop.Reviews)
                                    .ToListAsync();

            calculateRatingAverages();

        }

        public async Task<IActionResult> OnPostFindByNameAsync()
        {
            if (String.IsNullOrEmpty(Text))
            {
                Shop = new List<Shop>();

                return Page();
            }

            Shop = await _context.Shops
                        .Where(s => s.Name.ToUpper().Contains(Text.ToUpper()))
                        .Include(shop => shop.Reviews)
                        .OrderByDescending(shop => shop.CreateDate)
                        .ToListAsync();

            calculateRatingAverages();

            return Page();
        }

        public async Task<IActionResult> OnPostFindAllAsync()
        {
            Shop = await _context.Shops
                        .OrderByDescending(shop => shop.CreateDate)
                        .Include(shop => shop.Reviews)
                        .ToListAsync();

            calculateRatingAverages();

            return Page();
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
