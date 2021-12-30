using ArmorMaster.Buisiness.DTO.ModelsDTO;
using ArmorMaster.Buisiness.DTO.RequestDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmorMaster.Buisiness.Services.ServiceInterfaces
{
    public interface IPlayerService
    {
        public Task CreatePlayerAsync(CreatePlayerModel model);
        public Task<PlayerModel> GetPlayerByIdAsync(int id);
        public Task<IEnumerable<PlayerModel>> GetAllPlayersAsync();
        public Task UpdatePlayerAsync(ModifyPlayerModel model);
        public Task DeletePlayerAsync(int id);
    }
}
