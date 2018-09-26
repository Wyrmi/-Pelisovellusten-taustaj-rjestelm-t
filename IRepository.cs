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
        /*
        h
        h
        h
        */
        Task<Player[]> GetPlayersWithScore(int score);
        Task<Player> GetPlayerWithName(string name);
        Task<Player[]> FindPlayersWithItem(String itemname);
        Task<Player[]> FindPlayersWithItems(int items);
        Task<Player> SellItem(Guid id, Guid itemId, int score);
        Task<Player[]> TopTen();
        Task<int> GetCommonLevel();
    }
}