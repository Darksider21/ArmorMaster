using ArmorMaster.Buisiness.DTO.ModelsDTO;
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
    public class ItemUpgradeService : IItemUpgradeService
    {
        private readonly IItemRepository itemRepository;
        private readonly IConstantsService constantService;
        private readonly ICalculationService calculationService;

        public ItemUpgradeService(IItemRepository itemRepository, IConstantsService constantsService,ICalculationService calculationService)
        {
            this.itemRepository = itemRepository;
            this.constantService = constantsService;
            this.calculationService = calculationService;
        }

        public async Task<ItemModel> DowngradeItemLevelAsync(int itemId)
        {
            var item = await itemRepository.GetItemByIdAsync(itemId);
            if (item == null)
            {
                throw new InvalidIdException();
            }
            
            int levelsToDowngrade =  -1;
            var upgradedItem = calculationService.ApplyChangeOfUpgradeLevelToItem(item, levelsToDowngrade);


            await itemRepository.UpdateItemAsync(upgradedItem);
            return ObjectMapper.Mapper.Map<ItemModel>(upgradedItem);
        }

        public async Task<ItemModel> DowngradeMultipleItemLevelsAsync(int itemId, int numberOfLevelsToDowngrade)
        {
            var item = await itemRepository.GetItemByIdAsync(itemId);
            if (item == null)
            {
                throw new InvalidIdException();
            }
            // -1 to make it negative
            int levelsToDowngrade = numberOfLevelsToDowngrade * -1;
            var upgradedItem = calculationService.ApplyChangeOfUpgradeLevelToItem(item, levelsToDowngrade);


            await itemRepository.UpdateItemAsync(upgradedItem);
            return ObjectMapper.Mapper.Map<ItemModel>(upgradedItem);
        }

        public async Task<ItemModel> UpgradeItemLevelAsync(int itemId)
        {
            var item =  await itemRepository.GetItemByIdAsync(itemId);
            if (item == null)
            {
                throw new InvalidIdException();
            }

            const int levelsToUpgrade = 1;
            var upgradedItem = calculationService.ApplyChangeOfUpgradeLevelToItem(item, levelsToUpgrade);


            await itemRepository.UpdateItemAsync(upgradedItem);
            return ObjectMapper.Mapper.Map<ItemModel>(upgradedItem);
        }

        public async Task<ItemModel> UpgradeMultipleItemLevelsAsync(int itemId, int numberOfLevelsToUpgrade)
        {
            var item = await itemRepository.GetItemByIdAsync(itemId);
            if (item == null)
            {
                throw new InvalidIdException();
            }

             int levelsToUpgrade = numberOfLevelsToUpgrade;
            var upgradedItem = calculationService.ApplyChangeOfUpgradeLevelToItem(item, levelsToUpgrade);


            await itemRepository.UpdateItemAsync(upgradedItem);
            return ObjectMapper.Mapper.Map<ItemModel>(upgradedItem);
        }
    }
}
