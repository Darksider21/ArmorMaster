using ArmorMaster.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmorMaster.Data.Repository
{
    public interface IItemStatRepository
    {
        public Task<IEnumerable<ItemStat>> GetItemStatsByItemIdAsync(int itemId);
        public Task UpdateMultipleItemStatsAsync(IEnumerable<ItemStat> itemStats);
    }
}
