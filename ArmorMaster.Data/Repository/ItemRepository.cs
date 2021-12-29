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
    public class ItemRepository : Repository<Item>, IItemRepository
    {
        public ItemRepository(ArmorMasterContext dbContext) : base(dbContext)
        {
        }

        public Task CreateItemAsync(Item item)
        {
            throw new NotImplementedException();
        }

        public Task DeleteItemAsync(Item item)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Item>> GetAllItemsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Item> GetItemByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Item>> GetItemsByMultipleIdsAsync(int[] ids)
        {
            throw new NotImplementedException();
        }

        public Task UpdateItemAsync(Item item)
        {
            throw new NotImplementedException();
        }
    }
}
