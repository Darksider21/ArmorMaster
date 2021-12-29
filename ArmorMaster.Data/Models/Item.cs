using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmorMaster.Data.Models
{
   public class Item
    {
        public int ItemId { get; set; }
        public string Type { get; set; }
        public int Potential { get; set; }

        public ICollection<ItemStat> ItemStats { get; set; }
        public Player Player { get; set; }
    }
}
