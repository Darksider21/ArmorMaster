using ArmorMaster.Data.Data;
using ArmorMaster.Data.Models;
using ArmorMaster.Data.Repository.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmorMaster.Data.Repository
{
    public class ItemStatRepository : Repository<ItemStat>, IItemStatRepository
    {
        public ItemStatRepository(ArmorMasterContext dbContext) : base(dbContext)
        {
        }

        public async Task DeleteMultipleItemStatsAsync(IEnumerable<ItemStat> itemStats)
        {
             _dbContext.Set<ItemStat>().RemoveRange(itemStats);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<ItemStat>> GetItemStatsByItemIdAsync(int itemId)
        {
            return await _dbContext.ItemStats.Where(x => x.Item.ItemId.Equals(itemId)).ToListAsync();
        }

        public async Task UpdateMultipleItemStatsAsync(IEnumerable<ItemStat> itemStats)
        {
            _dbContext.Set<ItemStat>().UpdateRange(itemStats);
            await _dbContext.SaveChangesAsync();
        }
    }
}
