using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmorMaster.Buisiness.DTO.ModelsDTO.ConstantsModels
{
    public class EnchantmentBonusesForItemModel
    {
        public string ItemType { get; set; }
        public string AffectedByEnchantmentStatType { get; set; }
        public double InitialBonusPercentage { get; set; }
        public double StepIncreasePerLevel { get; set; }
    }
}
