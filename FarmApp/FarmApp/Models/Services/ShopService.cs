using FarmApp.Models.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FarmApp.Models.Services
{
    public class ShopService : IShopService
    {

        private readonly FarmApp.Data.FarmAppContext _context;

        public ShopService(FarmApp.Data.FarmAppContext context)
        {
            _context = context;
        }
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
                    shop.Street,
                    shop.City,
                    shop.PostalCode,
                    shop.Latitude,
                    shop.Longitude
                ));
            }

            return shopsDto;
        }
    }
}
