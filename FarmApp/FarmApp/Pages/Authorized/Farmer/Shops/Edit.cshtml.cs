using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FarmApp.Data;
using FarmApp.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace FarmApp.Pages.Authorized.Farmer.Shops
{
    /// <summary>
    /// PageModel for shop edit page.
    /// </summary>
    public class EditModel : PageModel
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
        public EditModel(FarmAppContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        /// <summary>
        /// Uploaded image of shop.
        /// </summary>
        [BindProperty]
        public BufferedSingleFileUploadDb Image { get; set; }

        /// <summary>
        /// Helping class for upload the image of shop.
        /// </summary>
        public class BufferedSingleFileUploadDb
        {
            [Display(Name = "Change Shop Thumbnail")]
            public IFormFile ImageFile { get; set; }
        }

        /// <summary>
        /// Current shop.
        /// </summary>
        [BindProperty]
        public Shop Shop { get; set; }

        /// <summary>
        /// Shows the shop settings with fields for edit.
        /// </summary>
        /// <param name="id">Shop id.</param>
        /// <returns>Page.</returns>
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

            if (!IsOwnerOfCurrentShop())
            {
                return NotFound();
            }

            return Page();
        }

        /// <summary>
        /// Processes form for editing shop.
        /// </summary>
        /// <returns>Page.</returns>
        public async Task<IActionResult> OnPostAsync()
        {
            if (Image.ImageFile != null)
            {
                byte[] imageByte = ConvertImageToByteArray();

                if (imageByte == null)
                {
                    return Page();
                }
                else
                {
                    Shop.Image = imageByte;
                }
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Shop).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShopExists(Shop.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        /// <summary>
        /// Checks existence of shop.
        /// </summary>
        /// <param name="id">Shop id.</param>
        /// <returns>true if shop exists, false otherwise.</returns>
        private bool ShopExists(int id)
        {
            return _context.Shops.Any(e => e.Id == id);
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

        /// <summary>
        /// Converts image to array of bytes.
        /// </summary>
        /// <returns>Array of bytes representing the image.</returns>
        private byte[] ConvertImageToByteArray()
        {
            using (var memoryStream = new MemoryStream())
            {
                var fileType = Image.ImageFile.ContentType;

                if (!(fileType.Equals("image/png") || fileType.Equals("image/jpg") || fileType.Equals("image/jpeg")))
                {
                    ModelState.AddModelError("File", "Incorrect format of uploaded file. Required formats are: png, jpg, jpeg.");
                    return null;
                }

                Image.ImageFile.CopyTo(memoryStream);

                // Upload the file if less than 2 MB
                if (memoryStream.Length < 2097152)
                {
                    return memoryStream.ToArray();
                }
                else
                {
                    ModelState.AddModelError("File", "The Shop Thumbnail is too large.");
                }
            }

            return null;
        }
    }
}
