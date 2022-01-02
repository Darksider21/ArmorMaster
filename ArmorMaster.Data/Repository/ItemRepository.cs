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
    public class ItemRepository : Repository<Item>, IItemRepository
    {
        public ItemRepository(ArmorMasterContext dbContext) : base(dbContext)
        {
        }

        public async Task CreateItemAsync(Item item)
        {
            await AddAsync(item);
        }

        public async Task DeleteItemAsync(Item item)
        {

            await DeleteAsync(item);
        }

        public async Task<IEnumerable<Item>> GetAllItemsAsync()
        {
            return await _dbContext.Items.Select(x => x).Include(x => x.ItemStats).ToListAsync();
        }

        public async Task<Item> GetItemByIdAsync(int id)
        {
            return await _dbContext.Items.Where(x => x.ItemId.Equals(id)).Include(x => x.ItemStats).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Item>> GetItemsByMultipleIdsAsync(int[] ids)
        {
            return await _dbContext.Items.Where(x => ids.Contains(x.ItemId)).Include(x => x.ItemStats).ToListAsync();
        }

        public async Task UpdateItemAsync(Item item)
        {
            await UpdateAsync(item);
        }
    }
}
