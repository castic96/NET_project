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

namespace FarmApp.Pages.Authorized.Farmer.Messages
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

        public async Task<IActionResult> OnGetAsync(int id1, string id2)
        {
            Shop = await _context.Shops.FirstOrDefaultAsync(shop => shop.Id == id1);
            LoadedUser = await _context.Users.FirstOrDefaultAsync(user => user.Id.Equals(id2));

            if (Shop == null || LoadedUser == null)
            {
                return NotFound();
            }

            return Page();
        }

        [BindProperty]
        public Message Message { get; set; }

        public Shop Shop { get; set; }
        public User LoadedUser { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(int id1, string id2)
        {
            Shop = await _context.Shops.FirstOrDefaultAsync(shop => shop.Id == id1);
            LoadedUser = await _context.Users.FirstOrDefaultAsync(user => user.Id.Equals(id2));

            if (Shop == null || LoadedUser == null)
            {
                return NotFound();
            }

            if (!IsOwnerOfCurrentShop())
            {
                return NotFound();
            }

            Message.User = LoadedUser;
            Message.Shop = Shop;
            Message.FromUser = false;

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Messages.Add(Message);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        private bool IsOwnerOfCurrentShop()
        {
            var loggedUser = _context.Users.Find(_userManager.GetUserId(User));
            var loggedUserShops = loggedUser.Shops;
            if (loggedUserShops != null && loggedUserShops.Contains(Shop)) return true;
            return false;
        }
    }
}
