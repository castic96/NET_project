using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FarmApp.Data;
using FarmApp.Models;

namespace FarmApp.Pages.Authorized.Farmer.Shops
{
    public class IndexModel : PageModel
    {
        private readonly FarmApp.Data.FarmAppContext _context;

        public IndexModel(FarmApp.Data.FarmAppContext context)
        {
            _context = context;
        }

        public IList<Shop> Shop { get;set; }

        public async Task OnGetAsync()
        {
            Shop = await _context.Shops.ToListAsync();
        }
    }
}
