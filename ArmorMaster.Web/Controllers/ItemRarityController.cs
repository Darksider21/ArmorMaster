using ArmorMaster.Buisiness.DTO.RequestDTO;
using ArmorMaster.Buisiness.Services.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArmorMaster.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemRarityController : ControllerBase
    {
        private readonly IItemRarityService itemRarityService;
        

        public ItemRarityController(IItemRarityService itemRarityService)
        {
            this.itemRarityService = itemRarityService;
        }


        [HttpGet]
        [Route("GetAvailiableItemRarities")]
        public IActionResult GetAvailiableItemRarities()
        {
            var rarities = itemRarityService.GetRarityTypesAndBonuses();
            return Ok(rarities);
        }

        [HttpPost]
        [Route("AddRarityToItem")]
        public async Task<IActionResult> AddRarityToItem(RarityItemModel rarityItemModel)
        {
            var item = await itemRarityService.AddRarityToItem(rarityItemModel);
            return Ok(item);
        }

        [HttpPut]
        [Route("ChangeItemsRarity")]
        public async Task<IActionResult> ChangeItemsRarity(RarityItemModel rarityItemModel)
        {
            var item = await itemRarityService.ChangeItemsRarity(rarityItemModel);
            return Ok(item);
        }
    }
}
