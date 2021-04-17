using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FarmApp.Data;
using FarmApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace FarmApp.Pages.Authorized.Customer.Reviews
{
    public class CreateModel : PageModel
    {
        private readonly FarmApp.Data.FarmAppContext _context;
        private readonly UserManager<User> _userManager;

        public CreateModel(FarmApp.Data.FarmAppContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public Review Review { get; set; }

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

            return Page();
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
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

            Review.Author = _context.Users.Find(_userManager.GetUserId(User));
            Review.Shop = _context.Shops.Find(Shop.Id);

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Reviews.Add(Review);
            await _context.SaveChangesAsync();

            return RedirectToPage($"./Index", new {id = Shop.Id});
        }
    }
}
