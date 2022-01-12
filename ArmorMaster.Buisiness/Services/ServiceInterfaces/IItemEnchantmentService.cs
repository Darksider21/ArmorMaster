using ArmorMaster.Buisiness.DTO.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmorMaster.Buisiness.Services.ServiceInterfaces
{
    public interface IItemEnchantmentService
    {
        public Task<ItemModel> IncreaseEnchantmentLevel(int itemId);
        public Task<ItemModel> DecreaseEnchantmentLevel(int itemId);

    }
}
