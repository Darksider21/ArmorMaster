using ArmorMaster.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmorMaster.Buisiness.Services.ServiceInterfaces
{
    public interface ICalculationService
    {
        public int GetNThTriangularNumber(int n);
        public Item ApplyChangeOfUpgradeLevelToItem(Item item, int upgradeDifference);
        public double GenerateBaseStatForItem(int itemLvl, double baseStatInitialValue);
        public void CalculateItemsFinalBaseStats(Item item);
        public void CalculateItemsFinalPotential(Item item);
    }
}
