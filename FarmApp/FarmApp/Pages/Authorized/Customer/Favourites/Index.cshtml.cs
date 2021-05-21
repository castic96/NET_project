using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FarmApp.Data;
using FarmApp.Models;
using Microsoft.AspNetCore.Identity;

namespace FarmApp.Pages.Authorized.Customer.Favourites
{
    /// <summary>
    /// PageModel for favourites index page.
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
        /// List of favourites shops for current customer.
        /// </summary>
        public IList<Favourite> Favourite { get;set; }

        /// <summary>
        /// Dictionary for average ratings for current list of favourite shops.
        /// </summary>
        public Dictionary<int, int> RatingAverages { get; set; }

        /// <summary>
        /// Shows favourite shops for current customer.
        /// </summary>
        /// <returns>Page.</returns>
        public async Task OnGetAsync()
        {
            Favourite = await _context.Favourites
                                    .Where(f => f.User.Id == _userManager.GetUserId(User))
                                    .Include(f => f.Shop)
                                    .Include(f => f.Shop.Reviews)
                                    .OrderByDescending(f => f.CreateDate)
                                    .ToListAsync();

            CalculateRatingAverages();

        }

        /// <summary>
        /// Calculates average ratings for current list of favourite shops.
        /// </summary>
        private void CalculateRatingAverages()
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
