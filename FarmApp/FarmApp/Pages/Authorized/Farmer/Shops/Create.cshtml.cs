using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FarmApp.Data;
using FarmApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.ComponentModel.DataAnnotations;

namespace FarmApp.Pages.Authorized.Farmer.Shops
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
        public BufferedSingleFileUploadDb Image { get; set; }

        public class BufferedSingleFileUploadDb
        {
            [Display(Name = "Shop Thumbnail")]
            public IFormFile ImageFile { get; set; }
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Shop Shop { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            // Fungujici alternativa:
            //Shop.Owner = _context.Users.FirstOrDefault(owner => owner.Id == _userManager.GetUserId(User));
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

        private byte[] ConvertImageToByteArray()
        {
            using (var memoryStream = new MemoryStream())
            {
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
