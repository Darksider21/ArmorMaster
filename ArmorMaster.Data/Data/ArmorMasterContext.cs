using ArmorMaster.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmorMaster.Data.Data
{
    public class ArmorMasterContext : DbContext
    {
        public ArmorMasterContext(DbContextOptions<ArmorMasterContext> options): base(options)
        {

        } 

        public DbSet<Player> Players { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemStat> ItemStats { get; set; }
    }
}
