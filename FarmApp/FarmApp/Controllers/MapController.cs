using FarmApp.Models.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FarmApp.Controllers
{
    /// <summary>
    /// Controller for map.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "Customers")]
    public class MapController : ControllerBase
    {
        /// <summary>
        /// Shop service.
        /// </summary>
        private readonly IShopService _shopService;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="shopService">Shop service.</param>
        public MapController(IShopService shopService)
        {
            _shopService = shopService;
        }

        /// <summary>
        /// Gets information about shops.
        /// </summary>
        /// <returns>Information about shops in JSON format.</returns>
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
