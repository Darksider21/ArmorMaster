using ArmorMaster.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmorMaster.Buisiness.DTO.ModelsDTO
{
    public class ItemModel
    {
        public int ItemId { get; set; }
        public string ItemType { get; set; }
        public int ItemPotential { get; set; }
        public int ItemLevel { get; set; }
        public string BaseStatType { get; set; }
        public double BaseStatQuantity { get; set; }


        public ICollection<ItemBonusStatModel> ItemBonusStats { get; set; }
        public Player Player { get; set; }
    }
}
