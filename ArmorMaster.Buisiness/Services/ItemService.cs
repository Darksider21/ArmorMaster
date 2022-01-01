using ArmorMaster.Buisiness.DTO.ModelsDTO;
using ArmorMaster.Buisiness.DTO.RequestDTO;
using ArmorMaster.Buisiness.Exceptions;
using ArmorMaster.Buisiness.Mapper;
using ArmorMaster.Buisiness.Services.ServiceInterfaces;
using ArmorMaster.Data.Models;
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
        private readonly IConstantsService constantsService;
        private readonly IItemStatService itemStatService;
        public ItemService(IItemRepository itemRepository, IConstantsService constantsService, IItemStatService itemStatService)
        {
            this.itemRepository = itemRepository;
            this.constantsService = constantsService;
            this.itemStatService = itemStatService;
        }

        public async Task<ItemModel> CreateItemAsync(CreateItemModel model)
        {
            if (!constantsService.ItemLevelIsValid(model.Level))
            {
                throw new InvalidItemLevelException();
            }
            if (!constantsService.ItemTypeExists(model.Type))
            {
                throw new InvalidItemTypeException();
            }
            var itemsPotential = constantsService.GetPotentialByItemLvlAndItemType(model.Level, model.Type);
            var newItem = new Item() { Level = model.Level, Type = model.Type, Potential = itemsPotential};
            var itemStats = await itemStatService.GenerateItemStatsForItemAsync(newItem);
            newItem.ItemStats = itemStats.ToList();
            await itemRepository.CreateItemAsync(newItem);

            return ObjectMapper.Mapper.Map<ItemModel>(newItem);
        }

        public async Task DeleteItemAsync(int id)
        {
            var item = await itemRepository.GetItemByIdAsync(id);
            if (item == null)
            {
                throw new InvalidIdException();
            }
            await itemRepository.DeleteItemAsync(item);
        }

        public async Task<IEnumerable<ItemModel>> GetAllItemsAsync()
        {
            var items = await itemRepository.GetAllItemsAsync();
            return ObjectMapper.Mapper.Map<IEnumerable<ItemModel>>(items);
        }

        public async Task<ItemModel> GetItemByIdAsync(int id)
        {
            var item = await itemRepository.GetItemByIdAsync(id);
            if (item == null)
            {
                throw new InvalidIdException();
            }
            return ObjectMapper.Mapper.Map<ItemModel>(item);
        }

        public async Task<IEnumerable<ItemModel>> GetItemsByMultipleIdsAsync(int[] ids)
        {
            var items = await itemRepository.GetItemsByMultipleIdsAsync(ids);
            if (items == null)
            {
                throw new InvalidIdException();
            }
            return ObjectMapper.Mapper.Map<IEnumerable<ItemModel>>(items);
        }
    }
}
