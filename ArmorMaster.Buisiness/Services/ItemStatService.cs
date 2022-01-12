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

        public   void  AdjustItemsBonusStatsToPotentialChange(Item item)
        {
            var potentialCostOfExistingBonusStats = GetItemPotentialByStats(item.ItemBonusStats);
            var potentialDifference = item.ItemPotential - potentialCostOfExistingBonusStats;
            if (potentialDifference == 0)
            {
                return;
            }
            var tempItem = new Item()
            {
                ItemLevel = item.ItemLevel,
                ItemPotential = Math.Abs(potentialDifference),
                ItemRarity = item.ItemRarity,
                ItemType = item.ItemType,
                ItemUpgradeLevel = item.ItemUpgradeLevel,
                EnchantmentLevel = item.EnchantmentLevel,
                BaseStatQuantity = item.BaseStatQuantity,
                BaseStatType = item.BaseStatType,
                ItemBonusStats = new List<ItemBonusStat>()
            };
            var newTempItemStatList = CreateBonusStatsForItem(tempItem).ToList();

            AddOrRemoveBonusStatsDependingOnPotentialDifference(item, potentialDifference, newTempItemStatList);

            foreach (var originalStat in item.ItemBonusStats)
            {
                if (originalStat.StatQuantity < 0)
                {
                    originalStat.StatQuantity = 0;
                }
            }

        }

        

        public async Task<IEnumerable<ItemBonusStatModel>> GenerateNewBonusStatsForItemAsync(int itemId)
        {
            var item = await itemRepository.GetItemByIdAsync(itemId);
            if (item == null)
            {
                throw new InvalidIdException();
            }
            var newItemStats = CreateBonusStatsForItem(item);
            var originalItemStats = item.ItemBonusStats;
            if (originalItemStats.Any())
            {
                MapNewItemStatsToOriginalStats(newItemStats, originalItemStats);
            }
            else
            {
                item.ItemBonusStats = newItemStats.ToList();
            }
            await itemStatRepository.UpdateMultipleItemStatsAsync(item.ItemBonusStats);

            return ObjectMapper.Mapper.Map<IEnumerable<ItemBonusStatModel>>(item.ItemBonusStats);
        }

        public  IEnumerable<ItemBonusStat> GenerateItemBonusStats(Item item)
        {
            var itemStats =  CreateBonusStatsForItem(item);


            return itemStats;

        }
        #region privateMethods
        private static void AddOrRemoveBonusStatsDependingOnPotentialDifference(Item item, int potentialDifference, List<ItemBonusStat> newTempItemStatList)
        {
            if (potentialDifference > 0)
            {
                foreach (var aditionalStat in newTempItemStatList)
                {
                    foreach (var originalBonusStat in item.ItemBonusStats)
                    {
                        if (aditionalStat.StatType.Equals(originalBonusStat.StatType) && originalBonusStat.StatType != "Critical Chance")
                        {
                            originalBonusStat.StatQuantity += aditionalStat.StatQuantity;
                        }
                    }
                }
            }
            if (potentialDifference < 0)
            {
                foreach (var aditionalStat in newTempItemStatList)
                {
                    foreach (var originalBonusStat in item.ItemBonusStats)
                    {
                        if (aditionalStat.StatType.Equals(originalBonusStat.StatType) && originalBonusStat.StatType != "Critical Chance")
                        {
                            originalBonusStat.StatQuantity -= aditionalStat.StatQuantity;
                        }
                    }
                }
            }
        }
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
        
        private static void MapNewItemStatsToOriginalStats(List<ItemBonusStat> newItemStats, ICollection<ItemBonusStat> originalItemStats)
        {
            foreach (var newItemStat in newItemStats)
            {
                if (!originalItemStats.Select(x => x.StatType).Contains(newItemStat.StatType))
                {
                    originalItemStats.Add(newItemStat);
                    newItemStats.Remove(newItemStat);
                }
            }

            foreach (var newItemStat in newItemStats)
            {
                foreach (var originalItemStat in originalItemStats)
                {
                    if (newItemStat.StatType.Equals(originalItemStat.StatType))
                    {
                        originalItemStat.StatQuantity = newItemStat.StatQuantity;
                    }
                }
            }



        }


        

        private List<ItemBonusStat> CreateBonusStatsForItem(Item item)
        {
            List<ItemBonusStat> itemStats = new List<ItemBonusStat>();
            var statCosts = constantsService.GetAvailiableItemStatCosts();
            
            var itemStatProportionModel = statCosts.Select(x => new ItemBonusStatProportionModel() {StatType = x.StatType , ProportionWeight = initialWeight });

            var modelWithAlocatedWeights = GiveBonusWeightToRandomStats(itemStatProportionModel).ToList();
            GiveBonusWeightToRandomStatsByRarity(modelWithAlocatedWeights , item.ItemRarity);

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
             var critChanceBonusStat =  GenerateCritChanceForItem(item);
            itemStats.Add(critChanceBonusStat);

            return itemStats;
        }

        private void GiveBonusWeightToRandomStatsByRarity(List<ItemBonusStatProportionModel> modelWithAlocatedWeights, string itemRarity)
        {
            if (String.IsNullOrWhiteSpace(itemRarity))
            {
                return;
            }
            var currentRarityBonus =  constantsService.GetItemRarityBonuses().Where(x => x.RarityName.Equals(itemRarity)).FirstOrDefault();
            foreach (var statType in modelWithAlocatedWeights)
            {
                foreach (var preferedBonusStat in currentRarityBonus.BonusStatPreferences)
                {
                    if (statType.StatType.Equals(preferedBonusStat))
                    {
                        statType.ProportionWeight *= currentRarityBonus.PreferedBonusStatsWeightIncreasePercentage;
                    }
                }
            }
            
        }

        private ItemBonusStat GenerateCritChanceForItem(Item item)
        {

            var itemRarityBonus = constantsService.GetItemRarityBonuses().Where(x => x.RarityName.Equals(item.ItemRarity)).FirstOrDefault();
            string critChanceName = "Critical Chance";
            int critChance = 0;
            int minCrit = 0 , maxCrit = 0;
            ItemBonusStat newCritChanceBonusStat = null;



            if (itemRarityBonus != null)
            {
                minCrit = maxCrit += itemRarityBonus.BaseCriticalChanceBonus;
            }
            var itemsThatCanGenerateCrit = constantsService.GetItemTypesThatCanGenrateCrit().Where(x => item.ItemType.Contains(x)).ToList();
            var critChanceByLevel = constantsService.GetItemCritChanceByLevel().Where(x => x.ItemLevel.Equals(item.ItemLevel)).FirstOrDefault();
            if (itemsThatCanGenerateCrit.Any())
            {
                minCrit += critChanceByLevel.MinChance;
                maxCrit += critChanceByLevel.MaxChance;
                critChance = randomProvider.Next(minCrit, maxCrit + 1);
                var test = new Random();
                
            }

            var existingCritChanceBonusStat = item.ItemBonusStats.Where(x => x.StatType.Equals(critChanceName)).FirstOrDefault();
            if (existingCritChanceBonusStat == null)
            {
                 newCritChanceBonusStat = new ItemBonusStat() { Item = item, StatQuantity = critChance, StatType = critChanceName };
                
            }
            else
            {

                var critBonusStat = existingCritChanceBonusStat;
                critBonusStat.StatQuantity = critChance;
                newCritChanceBonusStat = critBonusStat;
                

            }
            return newCritChanceBonusStat;
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
