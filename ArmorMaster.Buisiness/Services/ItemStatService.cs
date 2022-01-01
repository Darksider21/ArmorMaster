using ArmorMaster.Buisiness.Services.ServiceInterfaces;
using ArmorMaster.Data.Models;
using ArmorMaster.Data.Repository;
using ArmorMaster.Data.Repository.Base;
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
        private readonly IConstantsService constantsService;
        private readonly IItemStatTypeRepository itemStatTypeRepository;
        private readonly IRandomProvider randomProvider;
        public ItemStatService(IItemStatRepository itemStatRepository, IItemService itemService,
            IConstantsService constantsService, IItemStatTypeRepository itemStatTypeRepository,
            IRandomProvider randomProvider)
        {
            this.itemStatRepository = itemStatRepository;
            this.itemService = itemService;
            this.constantsService = constantsService;
            this.randomProvider = randomProvider;
        }

        public  async Task<IEnumerable<ItemStat>> GenerateLackingStatsForItemAsync(Item item)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ItemStat>> GenerateNewStatsForItemAsync(int itemId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ItemStat>> GenerateItemStatsForItemAsync(Item item)
        {
            var itemStats =  await CreateItemStatsForNewItem(item);

            var generatedItemStats = await GenerateItemStatsValues(itemStats);

        }

        private  Task<List<ItemStat>> GenerateItemStatsValues(List<ItemStat> itemStats)
        {
            var statCosts = constantsService.GetAvailiableItemStatCosts();
        }

        private  async Task<List<ItemStat>> CreateItemStatsForNewItem(Item item)
        {
            var availiableItemStatTypes = await itemStatTypeRepository.GetAllItemStatTypesAsync();
            List<ItemStat> itemStats = new List<ItemStat>();
            foreach (var itemStatType in availiableItemStatTypes)
            {
                var newItemStat = new ItemStat() { Item = item, ItemStatType = itemStatType };
                itemStats.Add(newItemStat);
            }
            return itemStats;
        }
    }
}
