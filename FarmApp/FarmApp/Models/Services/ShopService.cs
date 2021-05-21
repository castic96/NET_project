using FarmApp.Data;
using FarmApp.Models.DTO;
using System.Collections.Generic;
using System.Linq;

namespace FarmApp.Models.Services
{
    /// <summary>
    /// Service for load data about shop from database and convert to structure for transfer.
    /// </summary>
    public class ShopService : IShopService
    {
        /// <summary>
        /// Database context.
        /// </summary>
        private readonly FarmAppContext _context;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="context">Database context.</param>
        public ShopService(FarmAppContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Convert data about shops from database to structure for transfer.
        /// </summary>
        /// <returns>List of shops in structure for transfer.</returns>
        public List<ShopDTO> GetShopsJson()
        {
            List<ShopDTO> shopsDto = new List<ShopDTO>();
            IList<Shop> shops = _context.Shops
                        .OrderByDescending(shop => shop.CreateDate)
                        .ToList();

            foreach(Shop shop in shops)
            {
                shopsDto.Add(new ShopDTO(
                    shop.Id,
                    shop.Name,
                    shop.Description,
                    shop.Address,
                    shop.Latitude,
                    shop.Longitude
                ));
            }

            return shopsDto;
        }
    }
}
