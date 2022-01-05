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
        }

        
    }
}
