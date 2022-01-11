using ArmorMaster.Buisiness.Exceptions;
using ArmorMaster.Buisiness.Services.ServiceInterfaces;
using ArmorMaster.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmorMaster.Buisiness.Services
{
    public class CalculationService : ICalculationService
    {
        private readonly IConstantsService constantsService;

        public CalculationService(IConstantsService constantsService)
        {
            this.constantsService = constantsService;
        }
        public double GenerateBaseStatForItem(int itemLvl, double baseStatInitialValue)
        {
            var triangularNumberPerLevels = 20;
            double triangularNumberFromLevels = (itemLvl / triangularNumberPerLevels);
            int triangularNumberTofind = 1 + Convert.ToInt32(Math.Floor(triangularNumberFromLevels));
            int triangularNumber = GetNThTriangularNumber(triangularNumberTofind);
            double stat = triangularNumber * baseStatInitialValue;
            return stat;

        }

        public Item ApplyChangeOfUpgradeLevelToItem(Item item, int upgradeDifference)
        {
            var UpgradeLevels = constantsService.GetAvailiableItemUpgradeLevels();
            var itemsFutureUpgradeLevel = item.ItemUpgradeLevel + upgradeDifference;

            if (!UpgradeLevels.Select(x => x.UpgradeLevel).Contains(itemsFutureUpgradeLevel))
            {
                if (itemsFutureUpgradeLevel < UpgradeLevels.Min(x => x.UpgradeLevel))
                {
                    throw new UpgradeLevelException(itemsFutureUpgradeLevel.ToString());
                }
                else
                {
                    throw new UpgradeLevelException(itemsFutureUpgradeLevel.ToString());
                }
            }
            item.ItemUpgradeLevel = itemsFutureUpgradeLevel;
             CalculateItemsFinalBaseStats(item);


            return item;    
        }

        public int GetNThTriangularNumber(int n)
        {
            return ( n*n + n) / 2;
        }

        public void CalculateItemsFinalBaseStats(Item item)
        {
            CalculateBaseStatFromUpgradeLevels(item);
            CalculateBaseStatFromRarity(item);
            item.BaseStatQuantity = Math.Round(item.BaseStatQuantity, 2);
        }

        private void CalculateBaseStatFromRarity(Item item)
        {
            if (String.IsNullOrEmpty(item.ItemRarity))
            {
                return;
            }
            var currentRarityBonus = constantsService.GetItemRarityBonuses().Where(x => x.RarityName.Equals(item.ItemRarity)).FirstOrDefault();
            item.BaseStatQuantity *= currentRarityBonus.BaseStatMultiplyer;
        }

        private void CalculateBaseStatFromUpgradeLevels(Item item)
        {
            var upgradeLevels = constantsService.GetAvailiableItemUpgradeLevels();
            var itemsUpgradeLevels = upgradeLevels.Where(x => x.UpgradeLevel <= item.ItemUpgradeLevel).ToList();
            double sumOfcurrentUpgradeMultiplyers = itemsUpgradeLevels.Sum(x => x.BaseStatIncreasePercentage);
            double baseStatMultiplyerFromUpgradeLevels = (sumOfcurrentUpgradeMultiplyers / 100) + 1;

            var itemsBaseStats = CalculateItemsRawBaseStats(item);

            item.BaseStatQuantity =  (itemsBaseStats * baseStatMultiplyerFromUpgradeLevels);
        }

        private double CalculateItemsRawBaseStats(Item item)
        {
            var itemType =  constantsService.GetAvailiableItemTypes().Where(x => x.Type.Equals(item.ItemType)).FirstOrDefault();
            var baseStats = GenerateBaseStatForItem(item.ItemLevel, itemType.BaseStatInitialValue);
            return baseStats;
        }

        public void CalculateItemsFinalPotential(Item item)
        {
            item.ItemPotential = constantsService.GetPotentialByItemLvlAndItemType(item.ItemLevel, item.ItemType);
            ApplyRarityMultiplyer(item);
        }

        private void ApplyRarityMultiplyer(Item item)
        {
            var rarityBonus = constantsService.GetItemRarityBonuses().Where(x => x.RarityName.Equals(item.ItemRarity)).FirstOrDefault();
            if (rarityBonus == null)
            {
                return;
            }
            int potentialAfterRarityMultiplyer = Convert.ToInt32(rarityBonus.PotentialMultiplyer * item.ItemPotential);
            item.ItemPotential  = potentialAfterRarityMultiplyer;
        }
    }
}
