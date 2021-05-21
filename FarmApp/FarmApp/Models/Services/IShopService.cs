using FarmApp.Models.DTO;
using System.Collections.Generic;

namespace FarmApp.Models.Services
{
    /// <summary>
    /// Interface for ShopService.
    /// </summary>
    public interface IShopService
    {
        /// <summary>
        /// Convert data about shops from database to structure for transfer.
        /// </summary>
        /// <returns>List of shops in structure for transfer.</returns>
        public List<ShopDTO> GetShopsJson();
    }
}
