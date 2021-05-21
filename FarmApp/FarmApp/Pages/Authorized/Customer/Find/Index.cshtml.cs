using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FarmApp.Data;
using FarmApp.Models;

namespace FarmApp.Pages.Authorized.Customer.Find
{
    /// <summary>
    /// PageModel for find index page, it shows farm shops.
    /// </summary>
    public class IndexModel : PageModel
    {
        /// <summary>
        /// Database context.
        /// </summary>
        private readonly FarmAppContext _context;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="context">Database context.</param>
        public IndexModel(FarmAppContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Text for filtering the search.
        /// </summary>
        [BindProperty]
        public string Text { get; set; }

        /// <summary>
        /// List of shops.
        /// </summary>
        public IList<Shop> Shop { get;set; }

        /// <summary>
        /// Dictionary for average ratings for current list of shops.
        /// </summary>
        public Dictionary<int, int> RatingAverages { get; set; }

        /// <summary>
        /// Shows farm shops.
        /// </summary>
        /// <returns>Page.</returns>
        public async Task OnGetAsync()
        {
            Shop = await _context.Shops
                                    .OrderByDescending(shop => shop.CreateDate)
                                    .Include(shop => shop.Reviews)
                                    .ToListAsync();

            CalculateRatingAverages();

        }

        /// <summary>
        /// Processes the form for search by shop name.
        /// </summary>
        /// <returns>Page.</returns>
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

            CalculateRatingAverages();

            return Page();
        }

        /// <summary>
        /// Processes the form for search all shops.
        /// </summary>
        /// <returns>Page.</returns>
        public async Task<IActionResult> OnPostFindAllAsync()
        {
            Shop = await _context.Shops
                        .OrderByDescending(shop => shop.CreateDate)
                        .Include(shop => shop.Reviews)
                        .ToListAsync();

            CalculateRatingAverages();

            return Page();
        }

        /// <summary>
        /// Calculates average ratings for current list of shops.
        /// </summary>
        private void CalculateRatingAverages()
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
