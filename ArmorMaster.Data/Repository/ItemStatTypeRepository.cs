using ArmorMaster.Data.Data;
using ArmorMaster.Data.Models;
using ArmorMaster.Data.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmorMaster.Data.Repository
{
    public class ItemStatTypeRepository : Repository<ItemStatType>, IItemStatTypeRepository
    {
        public ItemStatTypeRepository(ArmorMasterContext dbContext) : base(dbContext)
        {
        }

        public Task<IEnumerable<ItemStatType>> GetAllItemStatTypesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ItemStatType> GetItemStatByNameAsync(string statName)
        {
            throw new NotImplementedException();
        }

        public Task<ItemStatType> GetItemStatTypeByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
