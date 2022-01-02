using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmorMaster.Data.Models
{
    public class ItemStat
    {
        public int ItemStatID { get; set; }

        public double StatQuantity { get; set; }

        public string StatType { get; set; }

        public Item Item { get; set; }
    }
}
