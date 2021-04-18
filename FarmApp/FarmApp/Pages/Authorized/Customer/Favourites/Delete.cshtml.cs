using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FarmApp.Data;
using FarmApp.Models;

namespace FarmApp.Pages.Authorized.Customer.Favourites
{
    public class DeleteModel : PageModel
    {
        private readonly FarmApp.Data.FarmAppContext _context;

        public DeleteModel(FarmApp.Data.FarmAppContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Favourite Favourite { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Favourite = await _context.Favourites.FirstOrDefaultAsync(m => m.Id == id);

            if (Favourite == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Favourite = await _context.Favourites.FindAsync(id);

            if (Favourite != null)
            {
                _context.Favourites.Remove(Favourite);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
