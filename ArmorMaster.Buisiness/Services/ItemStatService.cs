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
        const int NumberOfStatsWithIncreasedWeight = 2;
        const int initialWeight = 100;
        const int zeroPercent = 0;
        const int oneHundredPercent = 101;
        const double bonusWeight = 1.4;

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
            var originalItemStats = item.ItemBonusStats;
            var originalStatsPotential = GetItemPotentialByStats(originalItemStats);
            var potentialToSpend = item.ItemPotential - originalStatsPotential;
            if (potentialToSpend <=0)
            {
                await GenerateNewStatsForItemAsync(item.ItemId);
                return;
            }
            var tempItem = new Item() { ItemPotential = potentialToSpend };
            var additionalItemStats = GenerateItemBonusStats(tempItem);
            AddNewStatsToPrevious(originalItemStats, additionalItemStats);
            await itemStatRepository.UpdateMultipleItemStatsAsync(originalItemStats);


        }


        public async Task<IEnumerable<ItemBonusStatModel>> GenerateNewStatsForItemAsync(int itemId)
        {
            var item = await itemRepository.GetItemByIdAsync(itemId);
            if (item == null)
            {
                throw new InvalidIdException();
            }
            var newItemStats = CreateItemStatsForItem(item);
            var originalItemStats = item.ItemBonusStats;
            MapNewItemStatsToOriginalStats(newItemStats, originalItemStats);
            await itemStatRepository.UpdateMultipleItemStatsAsync(originalItemStats);

            return ObjectMapper.Mapper.Map<IEnumerable<ItemBonusStatModel>>(originalItemStats);
        }

        public  IEnumerable<ItemBonusStat> GenerateItemBonusStats(Item item)
        {
            var itemStats =  CreateItemStatsForItem(item);


            return itemStats;

        }
        #region privateMethods
        private int GetItemPotentialByStats(ICollection<ItemBonusStat> originalStats)
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
        private static void AddNewStatsToPrevious(ICollection<ItemBonusStat> originalStats, IEnumerable<ItemBonusStat> additionalItemStats)
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
        private static void MapNewItemStatsToOriginalStats(List<ItemBonusStat> newItemStats, ICollection<ItemBonusStat> originalItemStats)
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


        

        private List<ItemBonusStat> CreateItemStatsForItem(Item item)
        {
            List<ItemBonusStat> itemStats = new List<ItemBonusStat>();
            var statCosts = constantsService.GetAvailiableItemStatCosts();
            
            var itemStatProportionModel = statCosts.Select(x => new ItemBonusStatProportionModel() {StatType = x.StatType , ProportionWeight = initialWeight });
            var modelWithAlocatedWeights = GiveBonusWeightToRandomStats(itemStatProportionModel).ToList();
            var sumOfWeight = modelWithAlocatedWeights.Sum(x => x.ProportionWeight);
            modelWithAlocatedWeights.ForEach(x => x.ChanceToBePicked = (x.ProportionWeight / sumOfWeight) * 100 );
            int unspentPotential = item.ItemPotential;

            var generationModel = statCosts.Select(x => new StatGeneratorModel()
            {
                StatType = x.StatType,
                BaseAmountToAdd = x.StatAmount,
                TimesToAddBaseAmount = 0
            ,
                BaseStatCost = x.StatCost
            }).ToList();


            while (unspentPotential > 0)
            {
                for (int i = 0; i < modelWithAlocatedWeights.Count; i++)
                {
                    if (unspentPotential > 0)
                    {
                        bool chanceIsTriggered = randomProvider.Next(zeroPercent, oneHundredPercent) <= modelWithAlocatedWeights[i].ChanceToBePicked;
                        if (chanceIsTriggered)
                        {
                            var currentStatGenerationModel = generationModel.Where(x => x.StatType.Equals(modelWithAlocatedWeights[i].StatType)).FirstOrDefault();
                            currentStatGenerationModel.TimesToAddBaseAmount++;
                            unspentPotential -= currentStatGenerationModel.BaseStatCost;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }



            foreach (var statToGenerate in generationModel)
            {
                var statQuantity = statToGenerate.BaseAmountToAdd * statToGenerate.TimesToAddBaseAmount;
                var generatedItemStat = new ItemBonusStat() { StatType = statToGenerate.StatType, StatQuantity = statQuantity };
                
                itemStats.Add(generatedItemStat);
            }
            GenerateCritChanceForItem(item);


            return itemStats;
        }

        private void GenerateCritChanceForItem(Item item)
        {
            string critChanceName = "Critical Chance";
            int critChance = 0;
            int minCrit = 0 , maxCrit = 0;
            var itemsThatCanGenerateCrit = constantsService.GetItemTypesThatCanGenrateCrit().Where(x => item.ItemType.Contains(x));
            var critChanceByLevel = constantsService.GetItemCritChanceByLevel().Where(x => x.ItemLevel.Equals(item.ItemLevel)).FirstOrDefault();
            if (itemsThatCanGenerateCrit != null)
            {
                minCrit = critChanceByLevel.MinChance;
                maxCrit = critChanceByLevel.MaxChance;
                critChance = randomProvider.Next(minCrit, maxCrit + 1);
            }

            if (critChance > 0)
            {
                
                
                    var existingCritChanceBonusStat = item.ItemBonusStats.Where(x => x.StatType.Equals(critChanceName)).FirstOrDefault();
                    if (existingCritChanceBonusStat == null)
                    {
                        var newCritChanceBonusStat = new ItemBonusStat() { Item = item, StatQuantity = critChance, StatType = critChanceName };
                        item.ItemBonusStats.Add(newCritChanceBonusStat);
                    }
                    else
                    {
                        item.ItemBonusStats.Where(x => x.StatType.Equals(critChanceName)).FirstOrDefault().StatQuantity = critChance;

                    }
                
                
            }
            

            
            
        }

        private IEnumerable<ItemBonusStatProportionModel> GiveBonusWeightToRandomStats(IEnumerable<ItemBonusStatProportionModel> itemStatProportionModel)
        {
            var originalModel = itemStatProportionModel.ToList();
            var newModel = new List<ItemBonusStatProportionModel>();
            for (int i = 0; i < NumberOfStatsWithIncreasedWeight; i++)
            {
                Index randomStatIndex =  randomProvider.Next(originalModel.Count);
                originalModel[randomStatIndex].ProportionWeight *= bonusWeight;
                newModel.Add(originalModel[randomStatIndex]);
                originalModel.Remove(originalModel[randomStatIndex]);
            }
            newModel.AddRange(originalModel);
            return newModel;
        }
        #endregion
    }
}
