using ArmorMaster.Buisiness.DTO.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmorMaster.Buisiness.Services.ServiceInterfaces
{
    public interface IPlayerItemService
    {
        public Task<ItemOwnerModel> EquipItemOnPlayerAsync(int itemId, int playerId);
        public Task<ItemOwnerModel> UnequipItemFromPlayerAsync(int itemId, int playerId);
    }
}
