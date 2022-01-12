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
        public void CalculateItemsFinalPotential(Item item)
        {
            item.ItemPotential = constantsService.GetPotentialByItemLvlAndItemType(item.ItemLevel, item.ItemType);
            ApplyRarityMultiplyerToPotential(item);
            ApplyEnchantmentMultiplyerToPotential(item);
        }

        

        public void CalculateItemsFinalBaseStats(Item item)
        {
            CalculateItemsRawBaseStats(item);
            CalculateBaseStatFromUpgradeLevels(item);
            CalculateBaseStatFromRarity(item);
            CalculateBaseStatFromEnchantment(item);
            item.BaseStatQuantity = Math.Round(item.BaseStatQuantity, 2);
        }
        #region BaseStatCalculations
        private void CalculateBaseStatFromEnchantment(Item item)
        {
            if (item.EnchantmentLevel <= 0)
            {
                return;
            }
            var enchantmentBonus = constantsService.GetItemEnchantmentBonuses().Where(x => x.ItemType.Equals(item.ItemType)).FirstOrDefault();
            if (enchantmentBonus == null || enchantmentBonus.AffectedByEnchantmentStatType != "Base Stat")
            {
                return;
            }
            double multiplyerStepByLevel = 0;
            if (item.EnchantmentLevel > 1)
            {
                multiplyerStepByLevel = enchantmentBonus.StepIncreasePerLevel * item.EnchantmentLevel;

            }
            var baseEnchantmentByLevelMultiplyer = enchantmentBonus.InitialBonusPercentage * item.EnchantmentLevel;
            var enchantmentMuliplyer = ((baseEnchantmentByLevelMultiplyer + multiplyerStepByLevel) / 100) + 1;
            var baseStatAfterEnchantmentBonus = item.BaseStatQuantity * enchantmentMuliplyer;
            item.BaseStatQuantity = baseStatAfterEnchantmentBonus;
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
            var quantityAfterUpgradeLevels = item.BaseStatQuantity * baseStatMultiplyerFromUpgradeLevels;

            item.BaseStatQuantity = quantityAfterUpgradeLevels;
        }

        private void CalculateItemsRawBaseStats(Item item)
        {
            var itemType = constantsService.GetAvailiableItemTypes().Where(x => x.Type.Equals(item.ItemType)).FirstOrDefault();
            var baseStats = GenerateBaseStatForItem(item.ItemLevel, itemType.BaseStatInitialValue);
            item.BaseStatQuantity = baseStats;
        }
        #endregion


        #region PotentialCalculations
        private void ApplyEnchantmentMultiplyerToPotential(Item item)
        {
            if (item.EnchantmentLevel <=0)
            {
                return;
            }
            var enchantmentBonus = constantsService.GetItemEnchantmentBonuses().Where(x => x.ItemType.Equals(item.ItemType)).FirstOrDefault();
            if (enchantmentBonus.AffectedByEnchantmentStatType != "Potential")
            {
                return;
            }
            var multiplyerStepByLevel = enchantmentBonus.StepIncreasePerLevel * (item.EnchantmentLevel - 1);
            var baseMultiplyerPerLevel = enchantmentBonus.InitialBonusPercentage * item.EnchantmentLevel;
            var enchantmentMuliplyer = ((baseMultiplyerPerLevel + multiplyerStepByLevel) / 100) + 1;
            var potentialAfterEnchantmentMultiplyer = Convert.ToInt32(item.ItemPotential * enchantmentMuliplyer);
            item.ItemPotential = potentialAfterEnchantmentMultiplyer;

        }
        private void ApplyRarityMultiplyerToPotential(Item item)
        {
            var rarityBonus = constantsService.GetItemRarityBonuses().Where(x => x.RarityName.Equals(item.ItemRarity)).FirstOrDefault();
            if (rarityBonus == null)
            {
                return;
            }
            int potentialAfterRarityMultiplyer = Convert.ToInt32(rarityBonus.PotentialMultiplyer * item.ItemPotential);
            item.ItemPotential = potentialAfterRarityMultiplyer;
        }
        #endregion



    }
}
