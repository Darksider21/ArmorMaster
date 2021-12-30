using ArmorMaster.Data.Data;
using ArmorMaster.Data.Models;
using ArmorMaster.Data.Repository.Base;
using Microsoft.EntityFrameworkCore;
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

        public async Task CreatePlayerAsync(Player player)
        {
            await AddAsync(player);
        }

        public async Task DeletePlayerAsync(Player player)
        {
            await DeleteAsync(player);
        }

        public async Task<IEnumerable<Player>> GetAllPlayersAsync()
        {
            return await _dbContext.Players.Select(x => x).Include(x => x.EquipedItems).ToListAsync();
        }

        public async Task<Player> GetPlayerByIdAsync(int id)
        {
            return await _dbContext.Players.Where(x => x.PlayerID.Equals(id)).Include(x => x.EquipedItems).FirstOrDefaultAsync();
        }

        public async Task UpdatePlayerAsync(Player player)
        {
            await UpdateAsync(player);
        }
    }
}
