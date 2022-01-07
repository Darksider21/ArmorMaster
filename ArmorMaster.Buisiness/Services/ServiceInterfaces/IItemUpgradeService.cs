using ArmorMaster.Buisiness.DTO.ModelsDTO;
using ArmorMaster.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmorMaster.Buisiness.Services.ServiceInterfaces
{
    public interface IItemUpgradeService
    {
        public Task<ItemModel> UpgradeItemLevelAsync(int itemId);
        public Task<ItemModel> UpgradeMultipleItemLevelsAsync(int itemId, int numberOfLevelsToUpgrade);
        public Task<ItemModel> DowngradeItemLevelAsync(int itemId);
        public Task<ItemModel> DowngradeMultipleItemLevelsAsync(int itemId, int numberOfLevelsToDowngrade);

    }
}
