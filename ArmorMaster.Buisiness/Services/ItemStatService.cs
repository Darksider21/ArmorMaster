using ArmorMaster.Buisiness.Services.ServiceInterfaces;
using ArmorMaster.Data.Models;
using ArmorMaster.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmorMaster.Buisiness.Services
{
    public class ItemStatService : IItemStatService
    {
        private readonly IItemStatRepository itemStatRepository;
        private readonly IItemService itemService;
        public ItemStatService(IItemStatRepository itemStatRepository, IItemService itemService)
        {
            this.itemStatRepository = itemStatRepository;
            this.itemService = itemService;
        }

        

        public Task<IEnumerable<ItemStat>> GenerateLackingStatsForItemAsync(int itemId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ItemStat>> GenerateNewStatsForItem(int itemId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ItemStat>> GenerateNewStatsForItemAsync(int itemId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ItemStat>> GetItemStatsByItemIdAsync(int itemId)
        {
            throw new NotImplementedException();
        }
    }
}
