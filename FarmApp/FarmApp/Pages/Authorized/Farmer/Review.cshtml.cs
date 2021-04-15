using System;
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
    public class ReviewModel : PageModel
    {
        private readonly FarmApp.Data.FarmAppContext _context;

        public ReviewModel(FarmApp.Data.FarmAppContext context)
        {
            _context = context;
        }

        public IList<Review> Review { get;set; }
        public Shop Shop { get; set; }

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

            Review = await _context.Reviews
                                .Where(review => review.Shop.Id == id)
                                .Include(review => review.Author)
                                .OrderByDescending(review => review.CreateDate)
                                .ToListAsync();

            return Page();
        }
    }
}
