using ArmorMaster.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmorMaster.Data.Repository.Base
{
    public interface IItemRepository
    {
        public Task<IEnumerable<Item>> GetAllItemsAsync();
        public Task<Item> GetItemByIdAsync(int id);
        public Task<IEnumerable<Item>> GetItemsByMultipleIdsAsync(int[] ids);
        public Task CreateItemAsync(Item item);
        public Task UpdateItemAsync(Item item);
        public Task DeleteItemAsync(Item item);
    }
}
