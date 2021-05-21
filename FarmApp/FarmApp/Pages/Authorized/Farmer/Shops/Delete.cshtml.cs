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
    /// PageModel for shop delete page.
    /// </summary>
    public class DeleteModel : PageModel
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
        public DeleteModel(FarmAppContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        /// <summary>
        /// Shop to delete.
        /// </summary>
        [BindProperty]
        public Shop Shop { get; set; }

        /// <summary>
        /// Shows deletion page for current shop.
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

            return Page();
        }

        /// <summary>
        /// Processes form for deleting shop.
        /// </summary>
        /// <param name="id">Shop id.</param>
        /// <returns>Page.</returns>
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Shop = await _context.Shops.FindAsync(id);

            if (Shop == null)
            {
                return NotFound();
            }

            if (!IsOwnerOfCurrentShop())
            {
                return NotFound();
            }

            var relatedReviews = await _context.Reviews
                                    .Where(review => review.Shop == Shop)
                                    .ToListAsync();

            var relatedFavourites = await _context.Favourites
                                        .Where(f => f.Shop == Shop)
                                        .ToListAsync();

            _context.Reviews.RemoveRange(relatedReviews);
            _context.Favourites.RemoveRange(relatedFavourites);
            _context.Shops.Remove(Shop);

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
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
    }
}
