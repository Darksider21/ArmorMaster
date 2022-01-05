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
    public class ItemStatController : ControllerBase
    {
        public readonly IItemStatService itemStatService;

        public ItemStatController(IItemStatService itemStatService)
        {
            this.itemStatService = itemStatService;
        }

        [Route("GenrateNewStatsForItem")]
        [HttpPost]
        public async Task<IActionResult> GenerateNewStatsForItem(int itemId)
        {
            var itemStats = await itemStatService.GenerateNewStatsForItemAsync(itemId);
            return Ok(itemStats);
        }
    }
}
