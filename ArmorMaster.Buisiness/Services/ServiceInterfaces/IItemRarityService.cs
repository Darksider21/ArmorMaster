using ArmorMaster.Buisiness.DTO.ModelsDTO;
using ArmorMaster.Buisiness.DTO.ModelsDTO.ConstantsModels;
using ArmorMaster.Buisiness.DTO.RequestDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmorMaster.Buisiness.Services.ServiceInterfaces
{
    public interface IItemRarityService
    {
        public Task<ItemModel> AddRarityToItem(RarityItemModel model);
        public Task<ItemModel> ChangeItemsRarity(RarityItemModel model);
        public IEnumerable<RarityBonusesForItemModel> GetRarityTypesAndBonuses();

    }
}
