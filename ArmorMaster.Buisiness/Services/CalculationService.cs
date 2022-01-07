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
            var changedItem = ApplyAllUpgradesToItemsStats(item);


            return item;    
        }

        public int GetNThTriangularNumber(int n)
        {
            return ( n*n + n) / 2;
        }

        private Item ApplyAllUpgradesToItemsStats(Item item)
        {
            CalculateStatsFromUpgradeLevels(item);
            return item;
        }
        private void CalculateStatsFromUpgradeLevels(Item item)
        {
            var upgradeLevels = constantsService.GetAvailiableItemUpgradeLevels();
            var itemsUpgradeLevels = upgradeLevels.Where(x => x.UpgradeLevel <= item.ItemUpgradeLevel).ToList();
            double sumOfcurrentUpgradeMultiplyers = itemsUpgradeLevels.Sum(x => x.BaseStatIncreasePercentage);
            double baseStatMultiplyerFromUpgradeLevels = (sumOfcurrentUpgradeMultiplyers / 100) + 1;

            var itemsBaseStats = CalculateItemsRawBaseStats(item);

            item.BaseStatQuantity =  Math.Round((itemsBaseStats * baseStatMultiplyerFromUpgradeLevels) , 2);
        }

        private double CalculateItemsRawBaseStats(Item item)
        {
            var itemType =  constantsService.GetAvailiableItemTypes().Where(x => x.Type.Equals(item.ItemType)).FirstOrDefault();
            var baseStats = GenerateBaseStatForItem(item.ItemLevel, itemType.BaseStatInitialValue);
            return baseStats;
        }
    }
}
