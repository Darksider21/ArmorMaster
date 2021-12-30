using ArmorMaster.Buisiness.DTO.ModelsDTO;
using ArmorMaster.Buisiness.DTO.RequestDTO;
using ArmorMaster.Buisiness.Services.ServiceInterfaces;
using ArmorMaster.Data.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmorMaster.Buisiness.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository itemRepository;
        public ItemService(IItemRepository itemRepository)
        {
            this.itemRepository = itemRepository;
        }

        public Task CreateItemAsync(CreateItemModel model)
        {
            throw new NotImplementedException();
        }

        public Task DeleteItemAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ItemModel>> GetAllItemsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ItemModel> GetItemByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ItemModel>> GetItemsByMultipleIdsAsync(int[] ids)
        {
            throw new NotImplementedException();
        }
    }
}
