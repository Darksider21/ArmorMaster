using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmorMaster.Buisiness.DTO.ModelsDTO.ConstantsModels
{
    public class RarityBonusesForItemModel
    {
        public string RarityName { get; set; }
        public double PotentialMultiplyer { get; set; }
        public double BaseStatMultiplyer { get; set; }
        public IEnumerable<string> BonusStatPreferences { get; set; }
        public double PreferedBonusStatsWeightIncreasePercentage { get; set; }
        public int BaseCriticalChanceBonus { get; set; }
    }
}
