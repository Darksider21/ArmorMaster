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
    public class ItemUpgradeController : ControllerBase
    {
        private readonly IItemUpgradeService itemUpgradeService;

        public ItemUpgradeController(IItemUpgradeService itemUpgradeService)
        {
            this.itemUpgradeService = itemUpgradeService;
        }

        [Route("UpgradeItem")]
        [HttpPost]
        public async Task<IActionResult> UpgradeItem(int itemId)
        {
            var item = await itemUpgradeService.UpgradeItemLevelAsync(itemId);
            return Ok(item);
        }

        [Route("UpgradeItemMultipleTimes")]
        [HttpPost]
        public async Task<IActionResult> UpgradeItemMultipleTimes(int itemId, int amountOfTimesToUpgrade)
        {
            var item = await itemUpgradeService.UpgradeMultipleItemLevelsAsync(itemId, amountOfTimesToUpgrade);
            return Ok(item);
        }
        [Route("DowngradeItem")]
        [HttpDelete]
        public async Task<IActionResult> DowngradeItem(int itemId)
        {
            var item = await itemUpgradeService.DowngradeItemLevelAsync(itemId);
            return Ok(item);
        }
        [Route("DowngradeItemMultipleTimes")]
        [HttpDelete]
        public async Task<IActionResult> DowngradeItemMultipleTimes(int itemId, int amountOfTimesToDowngrade)
        {
            var item = await itemUpgradeService.DowngradeMultipleItemLevelsAsync(itemId, amountOfTimesToDowngrade);
            return Ok(item);
        }
    }
}
