using ArmorMaster.Buisiness.DTO.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmorMaster.Buisiness.Services.ServiceInterfaces
{
    public interface IItemStatTypeService
    {
        public Task<IEnumerable<ItemStatTypeModel>> GetAllItemStatTypesAsync();
        public Task<ItemStatTypeModel> GetItemStatTypeByIdAsync(int id);
        public Task<ItemStatTypeModel> GetItemStatByNameAsync(string statName);
    }
}
