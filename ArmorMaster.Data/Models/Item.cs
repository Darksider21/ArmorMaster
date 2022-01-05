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
        public string ItemType { get; set; }
        public int ItemPotential { get; set; }
        public int ItemLevel { get; set; }
        public string BaseStatType { get; set; }
        public double BaseStatQuantity { get; set; }


        public ICollection<ItemBonusStat> ItemBonusStats { get; set; }
        public Player Player { get; set; }
    }
}
