using ArmorMaster.Buisiness.DTO.ModelsDTO;
using ArmorMaster.Buisiness.DTO.RequestDTO;
using ArmorMaster.Buisiness.Exceptions;
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
        public async Task CreatePlayerAsync(CreatePlayerModel model)
        {
            throw new NotImplementedException();
        }

        public async Task DeletePlayerAsync(int id)
        {
            throw new NotImplementedException();
        }


        public async Task<IEnumerable<PlayerModel>> GetAllPlayersAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<PlayerModel> GetPlayerByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<ItemOwnerModel> EquipItemOnPlayerAsync(int itemId, int playerId)
        {
            var item = await itemRepository.GetItemByIdAsync(itemId);
            var player = await playerRepository.GetPlayerByIdAsync(playerId);
            if (item == null || player == null)
            {
                throw new InvalidIdException();
            }
            item.Player = null;
            await itemRepository.UpdateItemAsync(item);
            var itemOwnerModel = new ItemOwnerModel() { ItemId = item.ItemId, PlayerId = player.PlayerID };
            return itemOwnerModel;
        }

        public async Task<ItemOwnerModel> UnequipItemFromPlayerAsync(int itemId, int playerId)
        {
            var item = await itemRepository.GetItemByIdAsync(itemId);
            var player = await playerRepository.GetPlayerByIdAsync(playerId);
            if (item == null || player == null)
            {
                throw new InvalidIdException();
            }
            item.Player = player;
            await itemRepository.UpdateItemAsync(item);
            var itemOwnerModel = new ItemOwnerModel() { ItemId = item.ItemId, PlayerId = player.PlayerID };
            return itemOwnerModel;
            
        }

        public async Task UpdatePlayerAsync(ModifyPlayerModel model)
        {
            throw new NotImplementedException();
        }
    }
}
