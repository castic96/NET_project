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

namespace FarmApp.Pages.Authorized.Customer.Messages
{
    public class SentModel : PageModel
    {
        private readonly FarmApp.Data.FarmAppContext _context;
        private readonly UserManager<User> _userManager;

        public SentModel(FarmApp.Data.FarmAppContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<Message> Message { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Message = await _context.Messages
                                .Where(message => message.User.Id == _userManager.GetUserId(User) && message.FromUser == true)
                                .Include(message => message.Shop) 
                                .OrderByDescending(message => message.CreateDate)
                                .ToListAsync();

            return Page();
        }
    }
}
