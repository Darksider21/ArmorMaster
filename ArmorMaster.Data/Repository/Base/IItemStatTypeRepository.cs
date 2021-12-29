using ArmorMaster.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmorMaster.Data.Repository.Base
{
    public interface IItemStatTypeRepository
    {
        public Task<IEnumerable<ItemStatType>> GetAllItemStatTypesAsync();
        public Task<ItemStatType> GetItemStatTypeByIdAsync(int id);
        public Task<ItemStatType> GetItemStatByNameAsync(string statName);
    }
}
