using ArmorMaster.Buisiness.DTO.ModelsDTO;
using ArmorMaster.Buisiness.DTO.ModelsDTO.ConstantsModels;
using ArmorMaster.Buisiness.Exceptions;
using ArmorMaster.Buisiness.Mapper;
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
        private readonly IItemRepository itemRepository;

        public ItemStatService(IItemStatRepository itemStatRepository, 
            IConstantsService constantsService, 
            IRandomProvider randomProvider, IItemRepository itemRepository)
        {
            this.itemStatRepository = itemStatRepository;
            this.constantsService = constantsService;
            this.randomProvider = randomProvider;
            this.itemRepository = itemRepository;
        }

        public  async Task GenerateLackingStatsForItemAsync(Item item)
        {
            var Existingitem = await itemRepository.GetItemByIdAsync(item.ItemId);
            if (Existingitem == null)
            {
                throw new InvalidIdException();
            }
            var originalItemStats = item.ItemStats;
            var originalStatsPotential = GetItemPotentialByStats(originalItemStats);
            var potentialToSpend = item.Potential - originalStatsPotential;
            if (potentialToSpend <=0)
            {
                await GenerateNewStatsForItemAsync(item.ItemId);
                return;
            }
            var additionalItemStats = GenerateItemStatsByPotential(potentialToSpend);
            AddNewStatsToPrevious(originalItemStats, additionalItemStats);
            await itemStatRepository.UpdateMultipleItemStatsAsync(originalItemStats);


        }


        public async Task<IEnumerable<ItemStatModel>> GenerateNewStatsForItemAsync(int itemId)
        {
            var item = await itemRepository.GetItemByIdAsync(itemId);
            if (item == null)
            {
                throw new InvalidIdException();
            }
            var newItemStats = CreateItemStatsForItem(item.Potential);
            var originalItemStats = item.ItemStats;
            MapNewItemStatsToOriginalStats(newItemStats, originalItemStats);
            await itemStatRepository.UpdateMultipleItemStatsAsync(originalItemStats);

            return ObjectMapper.Mapper.Map<IEnumerable<ItemStatModel>>(originalItemStats);
        }

        public  IEnumerable<ItemStat> GenerateItemStatsByPotential(int potential)
        {
            var itemStats =  CreateItemStatsForItem(potential);


            return itemStats;

        }
        #region privateMethods
        private int GetItemPotentialByStats(ICollection<ItemStat> originalStats)
        {
            int potential = 0;
            var itemStatCosts = constantsService.GetAvailiableItemStatCosts();
            foreach (var originalItemStat in originalStats)
            {
                foreach (var itemStatCost in itemStatCosts)
                {
                    if (originalItemStat.StatType.Equals(itemStatCost.StatType))
                    {
                        int potentialCost =  Convert.ToInt32((originalItemStat.StatQuantity / itemStatCost.StatAmount))  * itemStatCost.StatCost;
                        potential += potentialCost;
                    }
                }
            }

            return potential;
        }
        private static void AddNewStatsToPrevious(ICollection<ItemStat> originalStats, IEnumerable<ItemStat> additionalItemStats)
        {
            foreach (var aditionalItemStat in additionalItemStats)
            {
                foreach (var originalItemStat in originalStats)
                {
                    if (aditionalItemStat.StatType.Equals(originalItemStat.StatType))
                    {
                        originalItemStat.StatQuantity += aditionalItemStat.StatQuantity;


                        if (originalItemStat.StatType.Equals("Critical Chance"))
                        {
                            originalItemStat.StatQuantity = Math.Round(originalItemStat.StatQuantity, 2);
                        }
                    }
                }
            }
        }
        private static void MapNewItemStatsToOriginalStats(List<ItemStat> newItemStats, ICollection<ItemStat> originalItemStats)
        {
            foreach (var newItemStat in newItemStats)
            {
                foreach (var originalItem in originalItemStats)
                {
                    if (originalItem.StatType.Equals(newItemStat.StatType))
                    {
                        originalItem.StatQuantity = newItemStat.StatQuantity;
                    }
                }
            }
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
                var generatedItemStat = new ItemStat() { StatType = statToGenerate.StatType, StatQuantity =  statQuantity };
                if (generatedItemStat.StatType == "Critical Chance")
                {
                    generatedItemStat.StatQuantity = Math.Round(statQuantity, 2);
                }
                itemStats.Add(generatedItemStat);
            }



            return itemStats;
        }
        #endregion
    }
}
