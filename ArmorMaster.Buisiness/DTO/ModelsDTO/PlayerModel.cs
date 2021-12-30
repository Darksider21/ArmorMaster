using ArmorMaster.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmorMaster.Buisiness.DTO.ModelsDTO
{
    public class PlayerModel
    {
        public int PlayerID { get; set; }
        public string PlayerName { get; set; }
        public ICollection<Item> EquipedItems { get; set; }
    }
}
