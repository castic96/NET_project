using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FarmApp.Data;
using FarmApp.Models;

namespace FarmApp.Pages.Authorized.Customer.Reviews
{
    public class IndexModel : PageModel
    {
        private readonly FarmApp.Data.FarmAppContext _context;

        public IndexModel(FarmApp.Data.FarmAppContext context)
        {
            _context = context;
        }

        public IList<Review> Reviews { get;set; }
        public Shop Shop { get; set; }

        public int RatingAverage { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Shop = await _context.Shops.FirstOrDefaultAsync(m => m.Id == id);

            if (Shop == null)
            {
                return NotFound();
            }

            Reviews = await _context.Reviews
                                .Where(review => review.Shop.Id == id)
                                .Include(review => review.Author)
                                .OrderByDescending(review => review.CreateDate)
                                .ToListAsync();

            calculateRatingAverages();

            return Page();
        }

        private void calculateRatingAverages()
        {
            RatingAverage = 0;

            if (Shop.Reviews != null && Shop.Reviews.Count > 0)
            {
                int sum = 0;

                foreach (var currentReview in Shop.Reviews)
                {
                    sum += currentReview.Rating;
                }

                RatingAverage = (int)((sum / (Shop.Reviews.Count * 5.0)) * 100);
            }
        }
    }
}
