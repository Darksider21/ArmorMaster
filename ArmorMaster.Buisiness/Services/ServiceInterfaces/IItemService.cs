using ArmorMaster.Buisiness.DTO.ModelsDTO;
using ArmorMaster.Buisiness.DTO.RequestDTO;
using ArmorMaster.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmorMaster.Buisiness.Services.ServiceInterfaces
{
    public interface IItemService
    {
        public Task<IEnumerable<ItemModel>> GetAllItemsAsync();
        public Task<ItemModel> GetItemByIdAsync(int id);
        public Task<IEnumerable<ItemModel>> GetItemsByMultipleIdsAsync(int[] ids);
        public Task CreateItemAsync(CreateItemModel model);
        public Task DeleteItemAsync(int id);
    }
}
