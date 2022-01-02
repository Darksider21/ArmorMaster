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
    public class ItemController : ControllerBase
    {

        private IItemService itemService;
        public ItemController(IItemService itemService)
        {
            this.itemService = itemService;
        }

        [Route("CreateItem")]
        [HttpPost]
        public async Task<IActionResult> CreateItem(CreateItemModel model)
        {
            if (ModelState.IsValid)
            {
                var item = await itemService.CreateItemAsync(model);
                return Ok(item);
            }
            return BadRequest();
        }
    }
}
