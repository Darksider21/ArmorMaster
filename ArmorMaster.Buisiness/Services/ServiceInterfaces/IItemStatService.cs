using ArmorMaster.Buisiness.DTO.ModelsDTO;
using ArmorMaster.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmorMaster.Buisiness.Services.ServiceInterfaces
{
    public interface IItemStatService
    {
        public IEnumerable<ItemBonusStat> GenerateItemStatsByPotential(int potential);
        public Task<IEnumerable<ItemBonusStatModel>> GenerateNewStatsForItemAsync(int itemId);
        public Task GenerateLackingStatsForItemAsync(Item item);
    }
}
