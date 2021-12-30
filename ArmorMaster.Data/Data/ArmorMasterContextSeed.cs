using ArmorMaster.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmorMaster.Data.Data
{
    public class ArmorMasterContextSeed
    {
        public static async Task SeedAsync(ArmorMasterContext context)
        {
            context.Database.Migrate();
            context.Database.EnsureCreated();
            var itemStats = GetPredefinedItemStatTypes();
            if (!context.ItemStatTypes.Any())
            {
                context.ItemStatTypes.AddRange(itemStats);
                await context.SaveChangesAsync();
            }
            var newItemsStats = itemStats.Where(x => !context.ItemStatTypes.Any(y => x.StatName == y.StatName));
            if (newItemsStats.Any())
            {
                context.ItemStatTypes.AddRange(newItemsStats);
                await context.SaveChangesAsync();
            }
        }

        private static IEnumerable<ItemStatType> GetPredefinedItemStatTypes()
        {
            return new List<ItemStatType>
            {
                new ItemStatType() {StatName ="Critical Chance"},
                new ItemStatType() {StatName ="Health"},
                new ItemStatType() {StatName ="Constitution"},
                new ItemStatType() {StatName ="Strength"},
                new ItemStatType() {StatName ="Agility"},
                new ItemStatType() {StatName ="Armor Pierce Chance"}
            };
        }
    }
}
