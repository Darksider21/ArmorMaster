using ArmorMaster.Buisiness.DTO.ModelsDTO;
using ArmorMaster.Buisiness.DTO.ModelsDTO.ConstantsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmorMaster.Buisiness.Services.ServiceInterfaces
{
    public interface IConstantsService
    {
        public IEnumerable<int> GetAvailiableItemLevels();
        public IEnumerable<string> GetAvailiableItemTypes();
        public IEnumerable<ItemStatCost> GetAvailiableItemStatCosts();
        public int GetPotentialByItemLvlAndItemType(int itemLvl,string itemType);
    }
}
