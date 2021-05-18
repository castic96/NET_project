using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FarmApp.Data;
using FarmApp.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace FarmApp.Pages.Authorized.Farmer.Shops
{
    public class EditModel : PageModel
    {
        private readonly FarmApp.Data.FarmAppContext _context;
        private readonly UserManager<User> _userManager;

        public EditModel(FarmApp.Data.FarmAppContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public BufferedSingleFileUploadDb Image { get; set; }

        public class BufferedSingleFileUploadDb
        {
            [Display(Name = "Change Shop Thumbnail")]
            public IFormFile ImageFile { get; set; }
        }

        [BindProperty]
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

            if (!IsOwnerOfCurrentShop())
            {
                return NotFound();
            }

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
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

        private bool ShopExists(int id)
        {
            return _context.Shops.Any(e => e.Id == id);
        }

        private bool IsOwnerOfCurrentShop()
        {
            var loggedUser = _context.Users.Find(_userManager.GetUserId(User));
            var loggedUserShops = loggedUser.Shops;
            if (loggedUserShops != null && loggedUserShops.Contains(Shop)) return true;
            return false;
        }

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
