using System;
using System.Threading.Tasks;

namespace webapi
{
    public interface IRepository
    {
        Task<Player> CreatePlayer(NewPlayer player);
        Task<Player> GetPlayer(Guid playerId);
        Task<Player[]> GetAllPlayers();
        Task<Player> UpdatePlayer(Guid id, ModifiedPlayer player);
        Task<Player> DeletePlayer(Guid playerId);

        Task<Item> CreateItem(Guid playerId, NewItem item);
        Task<Item> GetItem(Guid playerId, Guid itemId);
        Task<Item[]> GetAllItems(Guid playerId);
        Task<Item> UpdateItem(Guid id,Guid itemId, ModifiedItem item);
        Task<Item> DeleteItem(Guid playerid, Guid itemId);
    }
}