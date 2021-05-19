using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FarmApp.Data;
using FarmApp.Models;
using Microsoft.AspNetCore.Identity;

namespace FarmApp.Pages.Authorized.Farmer.Messages
{
    public class ReceivedModel : PageModel
    {
        private readonly FarmApp.Data.FarmAppContext _context;
        private readonly UserManager<User> _userManager;

        public ReceivedModel(FarmApp.Data.FarmAppContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<Message> Message { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            int[] shops = await _context.Shops
                                    .Where(shop => shop.Owner.Id == _userManager.GetUserId(User))
                                    .Select(shop => shop.Id)
                                    .ToArrayAsync();

            Message = await _context.Messages
                                .Where(message => shops.Contains(message.Shop.Id) && message.FromUser == true)
                                .Include(message => message.Shop)
                                .Include(message => message.User)
                                .OrderByDescending(message => message.CreateDate)
                                .ToListAsync();

            return Page();
        }
    }
}
