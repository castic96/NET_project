using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FarmApp.Data;
using FarmApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace FarmApp.Pages.Authorized.Customer.Favourites
{
    /// <summary>
    /// PageModel for add current shop to Favourites.
    /// </summary>
    public class CreateModel : PageModel
    {
        /// <summary>
        /// Databse context.
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
        public CreateModel(FarmAppContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        /// <summary>
        /// Shop to add to Favourites.
        /// </summary>
        [BindProperty]
        public Favourite Favourite { get; set; }

        /// <summary>
        /// Current shop.
        /// </summary>
        public Shop Shop { get; set; }

        /// <summary>
        /// Shows page to add shop to Favourites.
        /// </summary>
        /// <param name="id"></param>
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

            return Page();
        }

        /// <summary>
        /// Processes form for add shop to Favourites.
        /// </summary>
        /// <param name="id">Shop id.</param>
        /// <returns>Page.</returns>
        public async Task<IActionResult> OnPostAsync(int? id)
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

            Favourite.Shop = Shop;
            Favourite.User = _context.Users.Find(_userManager.GetUserId(User));
            Favourite.Id = 0;

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Favourites.Add(Favourite);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

    }
}
