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
    public class ItemStatRepository : Repository<ItemStat>, IItemStatRepository
    {
        public ItemStatRepository(ArmorMasterContext dbContext) : base(dbContext)
        {
        }

        public Task<IEnumerable<ItemStat>> GetItemStatsByItemIdAsync(int itemId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateMultipleItemStatsAsync(IEnumerable<ItemStat> itemStats)
        {
            throw new NotImplementedException();
        }
    }
}
