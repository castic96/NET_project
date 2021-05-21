using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FarmApp.Data;
using FarmApp.Models;

namespace FarmApp.Pages.Authorized.Customer.Favourites
{
    /// <summary>
    /// PageModel for remove shop from favourites for current customer.
    /// </summary>
    public class DeleteModel : PageModel
    {
        /// <summary>
        /// Database context.
        /// </summary>
        private readonly FarmAppContext _context;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="context">Database context.</param>
        public DeleteModel(FarmAppContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Favourite shop to remove.
        /// </summary>
        [BindProperty]
        public Favourite Favourite { get; set; }

        /// <summary>
        /// Shows removing page for current favourite shop.
        /// </summary>
        /// <param name="id">Favourite id.</param>
        /// <returns>Page.</returns>
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Favourite = await _context.Favourites.Include(f => f.Shop).FirstOrDefaultAsync(m => m.Id == id);

            if (Favourite == null)
            {
                return NotFound();
            }
            return Page();
        }

        /// <summary>
        /// Processes form for removing favourite shop.
        /// </summary>
        /// <param name="id">Favourite id.</param>
        /// <returns>Page.</returns>
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Favourite = await _context.Favourites.Include(f => f.Shop).FirstOrDefaultAsync(m => m.Id == id);

            if (Favourite != null)
            {
                _context.Favourites.Remove(Favourite);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
