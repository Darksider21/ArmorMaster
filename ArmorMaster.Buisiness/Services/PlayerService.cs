using ArmorMaster.Buisiness.DTO.ModelsDTO;
using ArmorMaster.Buisiness.DTO.RequestDTO;
using ArmorMaster.Buisiness.Services.ServiceInterfaces;
using ArmorMaster.Data.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmorMaster.Buisiness.Services
{
    public class PlayerService : IPlayerService, IPlayerItemService
    {
        private readonly IPlayerRepository playerRepository;
        private readonly IItemRepository itemRepository;
         
        public PlayerService(IPlayerRepository playerRepository, IItemRepository itemRepository)
        {
            this.playerRepository = playerRepository;
            this.itemRepository = itemRepository;
        }
        public Task CreatePlayerAsync(CreatePlayerModel model)
        {
            throw new NotImplementedException();
        }

        public Task DeletePlayerAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ItemOwnerModel> EquipItemOnPlayerAsync(int itemId, int playerId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PlayerModel>> GetAllPlayersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<PlayerModel> GetPlayerByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ItemOwnerModel> UnequipItemFromPlayerAsync(int itemId, int playerId)
        {
            throw new NotImplementedException();
        }

        public Task UpdatePlayerAsync(ModifyPlayerModel model)
        {
            throw new NotImplementedException();
        }
    }
}
