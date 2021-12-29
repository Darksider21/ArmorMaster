using ArmorMaster.Data.Data;
using ArmorMaster.Data.Models;
using ArmorMaster.Data.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmorMaster.Data.Repository
{
    public class PlayerItemRepository :IPlayerItemRepository
    {
        private readonly ArmorMasterContext dbContext;
        public PlayerItemRepository(ArmorMasterContext dbContext) 
        {
            this.dbContext = dbContext;
        }

        public Task EquipItemOnPlayerByIdAsync(int itemId, int playerId)
        {
            throw new NotImplementedException();
        }

        public Task UnequipItemFromPlayerByIdAsync(int itemId, int playerId)
        {
            throw new NotImplementedException();
        }
    }
}
