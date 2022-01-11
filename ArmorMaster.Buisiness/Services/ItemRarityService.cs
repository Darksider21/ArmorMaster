using ArmorMaster.Buisiness.DTO.ModelsDTO;
using ArmorMaster.Buisiness.DTO.ModelsDTO.ConstantsModels;
using ArmorMaster.Buisiness.DTO.RequestDTO;
using ArmorMaster.Buisiness.Exceptions;
using ArmorMaster.Buisiness.Mapper;
using ArmorMaster.Buisiness.Services.ServiceInterfaces;
using ArmorMaster.Data.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmorMaster.Buisiness.Services
{
    public class ItemRarityService : IItemRarityService
    {
        public readonly IItemRepository itemRepository;
        public readonly IConstantsService constantsService;
        public readonly ICalculationService calculationService;
        private readonly IItemStatService itemStatService;

        public ItemRarityService(IItemRepository itemRepository,IConstantsService constantsService ,
            ICalculationService calculationService , IItemStatService itemStatService)
        {
            this.itemRepository = itemRepository;
            this.constantsService = constantsService;
            this.calculationService = calculationService;
            this.itemStatService = itemStatService;
        }

        public async Task<ItemModel> AddRarityToItem(RarityItemModel model)
        {
            var item = await itemRepository.GetItemByIdAsync(model.ItemId);
            if (item == null)
            {
                throw new InvalidIdException();
            }
            if (item.ItemRarity != null)
            {
                throw new ItemAlreadyHaveRarityException();
            }
            var newRarityName = constantsService.GetItemsRarityTypes().Where(x => x.Equals(model.RarityName)).FirstOrDefault();
            if (newRarityName == null)
            {
                throw new InvalidRarityNameException();
            }
            ApplyRarityToItem(item, newRarityName);

            await itemRepository.UpdateItemAsync(item);

            return ObjectMapper.Mapper.Map<ItemModel>(item);

        }


        public async Task<ItemModel> ChangeItemsRarity(RarityItemModel model)
        {
            var item = await itemRepository.GetItemByIdAsync(model.ItemId);
            if (item == null)
            {
                throw new InvalidIdException();
            }
            var newRarityName = constantsService.GetItemsRarityTypes().Where(x => x.Equals(model.RarityName)).FirstOrDefault();
            if (newRarityName == null)
            {
                throw new InvalidRarityNameException();
            }
            ApplyRarityToItem(item, newRarityName);
            await itemRepository.UpdateItemAsync(item);

            return ObjectMapper.Mapper.Map<ItemModel>(item);
        }

        public IEnumerable<RarityBonusesForItemModel> GetRarityTypesAndBonuses()
        {
            var rarityBonusesList = constantsService.GetItemRarityBonuses().ToList();
            return rarityBonusesList;
        }
        private void ApplyRarityToItem(Data.Models.Item item, string newRarityName)
        {
            item.ItemRarity = newRarityName;
            calculationService.CalculateItemsFinalBaseStats(item);
            calculationService.CalculateItemsFinalPotential(item);
            var newItemsBonusStats = itemStatService.GenerateItemBonusStats(item);
            item.ItemBonusStats = newItemsBonusStats.ToList();
        }
    }
}
