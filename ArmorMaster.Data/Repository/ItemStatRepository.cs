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
    public class ItemStatRepository : Repository<ItemBonusStat>, IItemStatRepository
    {
        public ItemStatRepository(ArmorMasterContext dbContext) : base(dbContext)
        {
        }

        public async Task DeleteMultipleItemStatsAsync(IEnumerable<ItemBonusStat> itemStats)
        {
             _dbContext.Set<ItemBonusStat>().RemoveRange(itemStats);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<ItemBonusStat>> GetItemStatsByItemIdAsync(int itemId)
        {
            return await _dbContext.ItemStats.Where(x => x.Item.ItemId.Equals(itemId)).ToListAsync();
        }

        public async Task UpdateMultipleItemStatsAsync(IEnumerable<ItemBonusStat> itemStats)
        {
            _dbContext.Set<ItemBonusStat>().UpdateRange(itemStats);
            await _dbContext.SaveChangesAsync();
        }
    }
}
