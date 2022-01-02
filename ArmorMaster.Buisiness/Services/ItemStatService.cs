using ArmorMaster.Buisiness.DTO.ModelsDTO.ConstantsModels;
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
        private readonly IConstantsService constantsService;
        private readonly IRandomProvider randomProvider;
        public ItemStatService(IItemStatRepository itemStatRepository, 
            IConstantsService constantsService, 
            IRandomProvider randomProvider)
        {
            this.itemStatRepository = itemStatRepository;
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

        public  IEnumerable<ItemStat> GenerateItemStatsByPotential(int potential)
        {
            var itemStats =  CreateItemStatsForItem(potential);


            return itemStats;

        }

        

        private List<ItemStat> CreateItemStatsForItem(int potential)
        {
            List<ItemStat> itemStats = new List<ItemStat>();
            var existingItemStats = constantsService.GetAvailiableItemStatTypes();
            var statCosts = constantsService.GetAvailiableItemStatCosts();

            var generationModel = statCosts.Select(x => new StatGeneratorModel() { StatType = x.StatType, BaseAmountToAdd = x.StatAmount, TimesToAddBaseAmount = 0
            ,BaseStatCost = x.StatCost}).ToList();

            while(potential > 0)
            {
                var randomNumber = randomProvider.Next(generationModel.Count());
                 
                generationModel[randomNumber].TimesToAddBaseAmount++;
                potential -= generationModel[randomNumber].BaseStatCost;

            }
            foreach (var statToGenerate in generationModel)
            {
                var statQuantity = statToGenerate.BaseAmountToAdd * statToGenerate.TimesToAddBaseAmount;
                var generatedItemStat = new ItemStat() { StatType = statToGenerate.StatType, StatQuantity = statQuantity };
                itemStats.Add(generatedItemStat);
            }



            return itemStats;
        }
    }
}
