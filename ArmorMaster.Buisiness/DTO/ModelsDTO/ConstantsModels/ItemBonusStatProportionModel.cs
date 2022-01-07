using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmorMaster.Buisiness.DTO.ModelsDTO.ConstantsModels
{
    public class ItemBonusStatProportionModel
    {
        public string StatType { get; set; }
        public double ProportionWeight { get; set; }
        public double ChanceToBePicked { get; set; }
    }
}
