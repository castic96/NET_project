using FarmApp.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FarmApp.Models.Services
{
    public interface IShopService
    {
        public List<ShopDTO> GetShopsJson();
    }
}
