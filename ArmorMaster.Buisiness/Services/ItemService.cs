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
        private readonly IItemStatRepository itemStatRepository;
        private readonly ICalculationService calculationService;
        public ItemService(IItemRepository itemRepository, IConstantsService constantsService,
            IItemStatService itemStatService, IItemStatRepository itemStatRepository , ICalculationService calculationService)
        {
            this.itemRepository = itemRepository;
            this.constantsService = constantsService;
            this.itemStatService = itemStatService;
            this.itemStatRepository = itemStatRepository;
            this.calculationService = calculationService;
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
            var itemTypeModel = constantsService.GetAvailiableItemTypes().Where(x => x.Type.Equals(model.Type)).FirstOrDefault();
            var calculatedBaseStat =  calculationService.GenerateBaseStatForItem(model.Level, itemTypeModel.BaseStatInitialValue);
            var newItem = new Item() { ItemLevel = model.Level, ItemType = model.Type,
                ItemPotential = 0, ItemBonusStats = new List<ItemBonusStat>() , 
                BaseStatType = itemTypeModel.BaseStatType , BaseStatQuantity = calculatedBaseStat};
            calculationService.CalculateItemsFinalPotential(newItem);

            var itemBonusStats = itemStatService.GenerateItemBonusStats(newItem).ToList();
            itemBonusStats.ForEach(x => newItem.ItemBonusStats.Add(x));
            
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
            await itemStatRepository.DeleteMultipleItemStatsAsync(item.ItemBonusStats);
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
