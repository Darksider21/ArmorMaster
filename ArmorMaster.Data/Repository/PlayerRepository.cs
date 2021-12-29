using ArmorMaster.Data.Data;
using ArmorMaster.Data.Models;
using ArmorMaster.Data.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmorMaster.Data.Repository
{
    public class PlayerRepository : Repository<Player>, IPlayerRepository
    {
        public PlayerRepository(ArmorMasterContext dbContext) : base(dbContext)
        {
        }

        public Task CreatePlayerAsync(Player player)
        {
            throw new NotImplementedException();
        }

        public Task DeletePlayerAsync(Player player)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Player>> GetAllPlayersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Player> GetPlayerByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdatePlayerAsync(Player player)
        {
            throw new NotImplementedException();
        }
    }
}
