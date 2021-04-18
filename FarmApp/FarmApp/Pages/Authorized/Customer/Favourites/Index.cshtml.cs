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

namespace FarmApp.Pages.Authorized.Customer.Favourites
{
    public class IndexModel : PageModel
    {
        private readonly FarmApp.Data.FarmAppContext _context;
        private readonly UserManager<User> _userManager;

        public IndexModel(FarmApp.Data.FarmAppContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<Favourite> Favourite { get;set; }

        public async Task OnGetAsync()
        {
            Favourite = await _context.Favourites
                                    .Where(f => f.User.Id == _userManager.GetUserId(User))
                                    .Include(f => f.Shop)
                                    .OrderByDescending(f => f.CreateDate)
                                    .ToListAsync();
        }
    }
}
