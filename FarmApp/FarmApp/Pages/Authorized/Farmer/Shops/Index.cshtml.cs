using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FarmApp.Data;
using FarmApp.Models;
using Microsoft.AspNetCore.Identity;

namespace FarmApp.Pages.Authorized.Farmer.Shops
{
    /// <summary>
    /// PageModel for shops index page. It shows shops owned by current farmer.
    /// </summary>
    public class IndexModel : PageModel
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
        public IndexModel(FarmAppContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        /// <summary>
        /// List of shops for current farmer.
        /// </summary>
        public IList<Shop> Shop { get;set; }

        /// <summary>
        /// Dictionary for average ratings for current list of shops.
        /// </summary>
        public Dictionary<int, int> RatingAverages { get; set; }

        /// <summary>
        /// Shows shops owned by current farmer.
        /// </summary>
        /// <returns>Page.</returns>
        public async Task OnGetAsync()
        {
            Shop = await _context.Shops
                                    .Where(shop => shop.Owner.Id == _userManager.GetUserId(User))
                                    .Include(shop => shop.Reviews)
                                    .OrderByDescending(shop => shop.CreateDate)
                                    .ToListAsync();

            CalculateRatingAverages();

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
