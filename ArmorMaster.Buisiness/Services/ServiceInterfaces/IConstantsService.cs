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
        public IEnumerable<ItemType> GetAvailiableItemTypes();
        public IEnumerable<ItemUpgradeLevel> GetAvailiableItemUpgradeLevels();
        public IEnumerable<ItemStatCost> GetAvailiableItemStatCosts();
        public IEnumerable<ItemCritChanceByLevelModel> GetItemCritChanceByLevel();
        public IEnumerable<string> GetItemTypesThatCanGenrateCrit();
        public IEnumerable<string> GetAvailiableItemStatTypes();
        public int GetPotentialByItemLvlAndItemType(int itemLvl,string itemType);

        public bool ItemTypeExists(string type);
        public bool ItemLevelIsValid(int lvl);
        
    }
}
