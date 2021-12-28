using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmorMaster.Data.Models
{
    public class Player
    {
        public int PlayerID { get; set; }
        public string PlayerName { get; set; }
        public ICollection<Item> EquipedItems { get; set; }
    }
}
