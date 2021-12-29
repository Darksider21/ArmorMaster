using ArmorMaster.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmorMaster.Data.Repository.Base
{
    public interface IPlayerRepository
    {
        
        public Task CreatePlayerAsync(Player player);
        public Task<Player> GetPlayerByIdAsync(int id);
        public Task<IEnumerable<Player>> GetAllPlayersAsync();
        public Task UpdatePlayerAsync(Player player);
        public Task DeletePlayerAsync(Player player);

    }
}
