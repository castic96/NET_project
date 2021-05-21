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
    /// <summary>
    /// PageModel for received messages.
    /// </summary>
    public class ReceivedModel : PageModel
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
        /// Contructor.
        /// </summary>
        /// <param name="context">Database context.</param>
        /// <param name="userManager">User manager.</param>
        public ReceivedModel(FarmAppContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        /// <summary>
        /// List of received messages.
        /// </summary>
        public IList<Message> Message { get; set; }

        /// <summary>
        /// Shows received messages page for current user.
        /// </summary>
        /// <returns>Page.</returns>
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
