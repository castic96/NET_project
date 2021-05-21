using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FarmApp.Data;
using FarmApp.Models;

namespace FarmApp.Pages.Authorized.Farmer
{
    /// <summary>
    /// PageModel for farmer reviews. Farmers can only display reviews for their shops.
    /// </summary>
    public class ReviewsModel : PageModel
    {
        /// <summary>
        /// Database context.
        /// </summary>
        private readonly FarmAppContext _context;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="context">Database context.</param>
        public ReviewsModel(FarmAppContext context)
        {
            _context = context;
        }

        /// <summary>
        /// List of reviews for current shop.
        /// </summary>
        public IList<Review> Reviews { get;set; }

        /// <summary>
        /// Current shop.
        /// </summary>
        public Shop Shop { get; set; }

        /// <summary>
        /// Average rating for current shop.
        /// </summary>
        public int RatingAverage { get; set; }

        /// <summary>
        /// Shows reviews for current shop.
        /// </summary>
        /// <param name="id">Shop id.</param>
        /// <returns>Page.</returns>
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

            CalculateRatingAverage();

            return Page();
        }

        /// <summary>
        /// This method calculates average rating for current shop.
        /// </summary>
        private void CalculateRatingAverage()
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
