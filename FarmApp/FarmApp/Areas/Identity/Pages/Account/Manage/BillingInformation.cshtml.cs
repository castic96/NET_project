using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FarmApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FarmApp.Areas.Identity.Pages.Account.Manage
{
    [Authorize(Policy = "Farmers")]
    public partial class BillingInformationModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly FarmApp.Data.FarmAppContext _context;

        public BillingInformationModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            FarmApp.Data.FarmAppContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public class InputModel
        {
            [Required]
            [DataType(DataType.EmailAddress)]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "First Name")]
            public string FirstName { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Last Name")]
            public string LastName { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Street")]
            public string Street { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "City")]
            public string City { get; set; }

            [Required]
            [DataType(DataType.PostalCode)]
            [Display(Name = "Postal Code")]
            public int PostalCode { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var userEntity = await _context.Users.FirstOrDefaultAsync(u => u.Id.Equals(_userManager.GetUserId(User)));

            Input = new InputModel();

            Input.Email = userEntity.Email;
            Input.FirstName = userEntity.FirstName;
            Input.LastName = userEntity.LastName;
            Input.Street = userEntity.Street;
            Input.City = userEntity.City;
            Input.PostalCode = userEntity.PostalCode;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var userEntity = await _context.Users.FirstOrDefaultAsync(u => u.Id.Equals(_userManager.GetUserId(User)));

            userEntity.FirstName = Input.FirstName;
            userEntity.LastName = Input.LastName;
            userEntity.Street = Input.Street;
            userEntity.City = Input.City;
            userEntity.PostalCode = Input.PostalCode;

            _context.Users.Update(userEntity);

            await _context.SaveChangesAsync();
            await _signInManager.RefreshSignInAsync(user);

            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
