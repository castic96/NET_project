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
        public string GetShops()
        {
            string data = "showshops_callback({\"shops\":[";

            foreach(var shop in _shopService.GetShopsJson())
            {
                data += shop.ToString();

                data += ",";
            }

            data = data.Remove(data.Length - 1);

            data += "]})";
            
            return data;
        }

    }
}
