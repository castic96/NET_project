using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FarmApp.Data;
using FarmApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace FarmApp.Pages.Authorized.Farmer.Messages
{
    /// <summary>
    /// PageModel for a creation of new message.
    /// </summary>
    public class CreateModel : PageModel
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
        public CreateModel(FarmAppContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        /// <summary>
        /// Shows create page for message.
        /// </summary>
        /// <param name="id1">Shop id.</param>
        /// <param name="id2">User id.</param>
        /// <returns>Page.</returns>
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

        /// <summary>
        /// Message from current farmer and his shop.
        /// </summary>
        [BindProperty]
        public Message Message { get; set; }

        /// <summary>
        /// Current shop.
        /// </summary>
        public Shop Shop { get; set; }

        /// <summary>
        /// Loaded user.
        /// </summary>
        public User LoadedUser { get; set; }

        /// <summary>
        /// Processes form for creation message.
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns>Page.</returns>
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

        /// <summary>
        /// Check if the current user is owner of current shop.
        /// </summary>
        /// <returns>true if current user is owner of current shop, false otherwise.</returns>
        private bool IsOwnerOfCurrentShop()
        {
            var loggedUser = _context.Users.Find(_userManager.GetUserId(User));
            var loggedUserShops = loggedUser.Shops;
            if (loggedUserShops != null && loggedUserShops.Contains(Shop)) return true;
            return false;
        }
    }
}
