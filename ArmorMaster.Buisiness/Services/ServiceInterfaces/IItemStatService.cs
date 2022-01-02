﻿using ArmorMaster.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmorMaster.Buisiness.Services.ServiceInterfaces
{
    public interface IItemStatService
    {
        public IEnumerable<ItemStat> GenerateItemStatsByPotential(int potential);
        public Task<IEnumerable<ItemStat>> GenerateNewStatsForItemAsync(int itemId);
        public Task<IEnumerable<ItemStat>> GenerateLackingStatsForItemAsync(Item item);
    }
}
