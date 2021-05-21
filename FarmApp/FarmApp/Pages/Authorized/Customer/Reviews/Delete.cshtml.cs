using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FarmApp.Data;
using FarmApp.Models;
using Microsoft.AspNetCore.Identity;

namespace FarmApp.Pages.Authorized.Customer.Reviews
{
    /// <summary>
    /// PageModel for review delete page.
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
        /// Current review.
        /// </summary>
        [BindProperty]
        public Review Review { get; set; }

        /// <summary>
        /// Shows delete page for the current review.
        /// </summary>
        /// <param name="id">Review id.</param>
        /// <returns>Page.</returns>
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Review = await _context.Reviews
                                .Include(review => review.Shop)
                                .FirstOrDefaultAsync(m => m.Id == id);

            if (Review == null)
            {
                return NotFound();
            }

            if (!IsOwnerOfCurrentReview())
            {
                return NotFound();
            }

            return Page();
        }

        /// <summary>
        /// Processes form for deleting review.
        /// </summary>
        /// <param name="id">Review id.</param>
        /// <returns>Page.</returns>
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Review = await _context.Reviews
                    .Include(review => review.Shop)
                    .FirstOrDefaultAsync(m => m.Id == id);

            if (Review == null)
            {
                return NotFound();
            }

            if (!IsOwnerOfCurrentReview())
            {
                return NotFound();
            }

            int shopId = Review.Shop.Id;
            _context.Reviews.Remove(Review);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index", new { id = shopId });
        }

        /// <summary>
        /// Check if the current user is owner of current review.
        /// </summary>
        /// <returns>true if current user is owner of current review, false otherwise.</returns>
        private bool IsOwnerOfCurrentReview()
        {
            var loggedUser = _context.Users.Find(_userManager.GetUserId(User));
            var loggedUserReviews = loggedUser.Reviews;
            if (loggedUserReviews != null && loggedUserReviews.Contains(Review)) return true;
            return false;
        }
    }
}
