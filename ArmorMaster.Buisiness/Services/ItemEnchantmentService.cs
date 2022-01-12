using ArmorMaster.Buisiness.DTO.ModelsDTO;
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
    public class ItemEnchantmentService : IItemEnchantmentService
    {
        public readonly IConstantsService constantsService;
        public readonly IItemRepository itemRepository;
        public readonly ICalculationService calculationService;
        public readonly IItemStatService itemStatService;

        public ItemEnchantmentService(IConstantsService constantsService, IItemRepository itemRepository, ICalculationService calculationService ,
            IItemStatService itemStatService)
        {
            this.constantsService = constantsService;
            this.itemRepository = itemRepository;
            this.calculationService = calculationService;
            this.itemStatService = itemStatService;
        }

        public async Task<ItemModel> DecreaseEnchantmentLevel(int itemId)
        {
            var item = await itemRepository.GetItemByIdAsync(itemId);
            if (item == null)
            {
                throw new InvalidIdException();
            }

            item.EnchantmentLevel -= 1;

            var maxEnchantmentLevel = constantsService.GetMaximumItemEnchantmentLevel();
            if (maxEnchantmentLevel < item.EnchantmentLevel)
            {
                throw new ItemReachedMaximumEnchantmentLevelException();
            }
            calculationService.CalculateItemsFinalBaseStats(item);
            calculationService.CalculateItemsFinalPotential(item);
            itemStatService.AdjustItemsBonusStatsToPotentialChange(item);


            await itemRepository.UpdateItemAsync(item);

            return ObjectMapper.Mapper.Map<ItemModel>(item);
        }

        public async Task<ItemModel> IncreaseEnchantmentLevel(int itemId)
        {
            var item = await itemRepository.GetItemByIdAsync(itemId);
            if (item == null)
            {
                throw new InvalidIdException();
            }

            item.EnchantmentLevel += 1;

            var maxEnchantmentLevel = constantsService.GetMaximumItemEnchantmentLevel();
            if (maxEnchantmentLevel < item.EnchantmentLevel)
            {
                throw new ItemReachedMaximumEnchantmentLevelException();
            }
            calculationService.CalculateItemsFinalBaseStats(item);
            calculationService.CalculateItemsFinalPotential(item);
            itemStatService.AdjustItemsBonusStatsToPotentialChange(item);


            await itemRepository.UpdateItemAsync(item);

            return ObjectMapper.Mapper.Map<ItemModel>(item);
            
        }
    }
}
