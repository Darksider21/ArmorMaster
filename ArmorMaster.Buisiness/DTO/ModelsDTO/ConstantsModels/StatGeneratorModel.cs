using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmorMaster.Buisiness.DTO.ModelsDTO.ConstantsModels
{
    public class StatGeneratorModel
    {
        public string StatType { get; set; }
        public int TimesToAddBaseAmount { get; set; }
        public double BaseAmountToAdd { get; set; }
        public int BaseStatCost { get; set; }
    }
}
