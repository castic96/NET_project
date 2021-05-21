using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FarmApp.Data;
using FarmApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.ComponentModel.DataAnnotations;

namespace FarmApp.Pages.Authorized.Farmer.Shops
{
    /// <summary>
    /// PageModel for shop create page.
    /// </summary>
    public class CreateModel : PageModel
    {
        /// <summary>
        /// Databse context.
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
        /// Uploaded image of shop.
        /// </summary>
        [BindProperty]
        public BufferedSingleFileUploadDb Image { get; set; }

        /// <summary>
        /// Helping class for upload the image of shop.
        /// </summary>
        public class BufferedSingleFileUploadDb
        {
            [Display(Name = "Shop Thumbnail")]
            public IFormFile ImageFile { get; set; }
        }

        /// <summary>
        /// Shows create page for shop.
        /// </summary>
        /// <returns>Page.</returns>
        public IActionResult OnGet()
        {
            return Page();
        }

        /// <summary>
        /// Current shop.
        /// </summary>
        [BindProperty]
        public Shop Shop { get; set; }

        /// <summary>
        /// Processes form for creating shop.
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

            Shop.Owner = _context.Users.Find(_userManager.GetUserId(User));

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Shops.Add(Shop);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
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
