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
    /// <summary>
    /// PageModel for shop details.
    /// </summary>
    public class DetailsModel : PageModel
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
        public DetailsModel(FarmAppContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        /// <summary>
        /// Current shop.
        /// </summary>
        public Shop Shop { get; set; }

        /// <summary>
        /// List of reviews for current shop.
        /// </summary>
        public IList<Review> Reviews { get; set; }

        /// <summary>
        /// Average rating for current shop.
        /// </summary>
        public int RatingAverage { get; set; }

        /// <summary>
        /// Shows detail of current shop.
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

            if (!IsOwnerOfCurrentShop())
            {
                return NotFound();
            }

            Reviews = await _context.Reviews
                    .Where(review => review.Shop.Id == id)
                    .OrderByDescending(review => review.CreateDate)
                    .Take(3)
                    .ToListAsync();

            CalculateRatingAverages();

            if (Reviews.Any())
            {
                CutReviewText();
            }

            return Page();
        }

        /// <summary>
        /// Cuts review text to maximum of 100 chars.
        /// </summary>
        private void CutReviewText()
        {
            int maxLength = 100;

            foreach (var currentReview in Reviews)
            {
                var currentComment = currentReview.Comment;

                if (currentComment.Length > maxLength)
                {
                    currentComment = currentComment.Substring(0, maxLength - 3) + "...";
                    currentReview.Comment = currentComment;
                }
            }
        }

        /// <summary>
        /// Check if the current user is owner of current shop.
        /// </summary>
        /// <returns>true if current user is owner of current shop, false otherwise.</returns>
        private bool IsOwnerOfCurrentShop()
        {
            var loggedUser = _context.Users.Find(_userManager.GetUserId(User));
            var loggedUserShops = loggedUser.Shops;
            if (loggedUserShops != null && loggedUserShops.Contains(Shop)) return true;
            return false;
        }

        /// <summary>
        /// Calculates average ratings for current list of shops.
        /// </summary>
        private void CalculateRatingAverages()
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
