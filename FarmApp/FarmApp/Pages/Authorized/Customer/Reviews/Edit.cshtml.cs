using System.Linq;
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
    /// PageModel for review edit page.
    /// </summary>
    public class EditModel : PageModel
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
        public EditModel(FarmAppContext context, UserManager<User> userManager)
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
        /// Shows the review values with fields for edit.
        /// </summary>
        /// <param name="id"></param>
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
        /// Processes form for editing review.
        /// </summary>
        /// <returns>Page.</returns>
        public async Task<IActionResult> OnPostAsync()
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Review).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReviewExists(Review.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            _context.Entry(Review).Reload();
            _context.Entry(Review).Reference(review => review.Shop).Load();

            return RedirectToPage("./Index", new { id = Review.Shop.Id });
        }

        /// <summary>
        /// Check existence of review.
        /// </summary>
        /// <param name="id">Review id.</param>
        /// <returns>true if review exists, false otherwise.</returns>
        private bool ReviewExists(int id)
        {
            return _context.Reviews.Any(e => e.Id == id);
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
