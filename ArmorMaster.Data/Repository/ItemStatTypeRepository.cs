using ArmorMaster.Data.Data;
using ArmorMaster.Data.Models;
using ArmorMaster.Data.Repository.Base;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<ItemStatType>> GetAllItemStatTypesAsync()
        {
            return await _dbContext.ItemStatTypes.Select(x => x).ToListAsync();
        }

        public async Task<ItemStatType> GetItemStatByNameAsync(string statName)
        {
            return await _dbContext.ItemStatTypes.Where(x => x.StatName == statName).FirstOrDefaultAsync();
        }

        public async Task<ItemStatType> GetItemStatTypeByIdAsync(int id)
        {
            return await _dbContext.ItemStatTypes.Where(x => x.ItemStatTypeId.Equals(id)).FirstOrDefaultAsync();
        }
    }
}
