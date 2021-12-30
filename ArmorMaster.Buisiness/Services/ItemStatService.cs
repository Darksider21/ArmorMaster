using ArmorMaster.Buisiness.Services.ServiceInterfaces;
using ArmorMaster.Data.Models;
using ArmorMaster.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmorMaster.Buisiness.Services
{
    public class ItemStatService : IItemStatService
    {
        private readonly IItemStatRepository itemStatRepository;
        public ItemStatService(IItemStatRepository itemStatRepository)
        {
            this.itemStatRepository = itemStatRepository;
        }

        public Task<IEnumerable<ItemStat>> GetItemStatsByItemIdAsync(int itemId)
        {
            throw new NotImplementedException();
        }
    }
}
