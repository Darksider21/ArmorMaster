using ArmorMaster.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmorMaster.Data.Repository.Base
{
    public interface IItemStatRepository
    {
        public Task<IEnumerable<ItemBonusStat>> GetItemStatsByItemIdAsync(int itemId);
        public Task UpdateMultipleItemStatsAsync(IEnumerable<ItemBonusStat> itemStats);
        public Task DeleteMultipleItemStatsAsync(IEnumerable<ItemBonusStat> itemStats);
    }
}
