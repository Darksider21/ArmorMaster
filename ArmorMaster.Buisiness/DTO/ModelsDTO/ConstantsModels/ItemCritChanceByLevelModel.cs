using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmorMaster.Buisiness.DTO.ModelsDTO.ConstantsModels
{
    public class ItemCritChanceByLevelModel
    {
        public int ItemLevel { get; set; }
        public int MinChance { get; set; }
        public int MaxChance { get; set; }
    }
}
