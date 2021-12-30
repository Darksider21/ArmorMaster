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
        public string Type { get; set; }
        public int Potential { get; set; }
        public int Level { get; set; }

        public ICollection<ItemStat> ItemStats { get; set; }
        public Player Player { get; set; }
    }
}
