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
    public class ItemEnchantmentController : ControllerBase
    {
        public readonly IItemEnchantmentService itemEnchantmentService;

        public ItemEnchantmentController(IItemEnchantmentService itemEnchantmentService)
        {
            this.itemEnchantmentService = itemEnchantmentService;
        }

        [HttpPost]
        [Route("IncreaseItemEnchantmentLevel")]
        public async Task<IActionResult> IncreaseItemEnchantmentLevel(int itemId)
        {
            var item = await itemEnchantmentService.IncreaseEnchantmentLevel(itemId);
            return Ok(item);
        }

        [HttpDelete]
        [Route("DecreaseItemEnchantmentLevel")]
        public async Task<IActionResult> DecreaseItemEnchantmentLevel(int itemId)
        {
            var item = await itemEnchantmentService.DecreaseEnchantmentLevel(itemId);
            return Ok(item);
        }
    }
}
