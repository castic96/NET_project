using FarmApp.Models.DTO;
using FarmApp.Models.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FarmApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "Customers")]
    public class MapController : ControllerBase
    {
        private readonly IShopService _shopService;
        public MapController(IShopService shopService)
        {
            _shopService = shopService;
        }

        [HttpGet]
        public List<ShopDTO> GetShops()
        {
            return _shopService.GetShopsJson();
        }

    }
}
