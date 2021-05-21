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
    /// <summary>
    /// PageModel for sent messages.
    /// </summary>
    public class SentModel : PageModel
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
        public SentModel(FarmAppContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        /// <summary>
        /// List of sent messages.
        /// </summary>
        public IList<Message> Message { get; set; }

        /// <summary>
        /// Shows sent messages page for current customer.
        /// </summary>
        /// <returns>Page.</returns>
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
