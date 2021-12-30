using ArmorMaster.Buisiness.DTO.ModelsDTO;
using ArmorMaster.Data.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmorMaster.Buisiness.Services.ServiceInterfaces
{
    public class ItemStatTypeService : IItemStatTypeService
    {
        private readonly IItemStatTypeRepository itemStatTypeRepository;

        public ItemStatTypeService(IItemStatTypeRepository itemStatTypeRepository)
        {
            this.itemStatTypeRepository = itemStatTypeRepository;
        }

        public Task<IEnumerable<ItemStatTypeModel>> GetAllItemStatTypesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ItemStatTypeModel> GetItemStatByNameAsync(string statName)
        {
            throw new NotImplementedException();
        }

        public Task<ItemStatTypeModel> GetItemStatTypeByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
