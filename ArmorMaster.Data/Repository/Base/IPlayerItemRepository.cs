using ArmorMaster.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmorMaster.Data.Repository.Base
{
    public interface IPlayerItemRepository
    {
        public Task EquipItemOnPlayerByIdAsync(int itemId, int playerId);
        public Task UnequipItemFromPlayerByIdAsync(int itemId, int playerId);

    }
}
