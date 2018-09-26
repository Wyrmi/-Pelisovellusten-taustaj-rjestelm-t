using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace webapi{

    public class Player
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        public bool IsBanned { get; set; }
        public DateTime CreationTime { get; set; }
        public List<Item> items { get; set; }
        public Player(){
           items= new List<Item>();
           CreationTime = DateTime.Now;
           Id = Guid.NewGuid ();
        }
    }
    public class NewPlayer
    {
        public string Name { get; set; }
    }
    public class ModifiedPlayer
    {
        public int Score { get; set; }
    }
    public class PlayersProcessor
    {
        IRepository repo;
        public PlayersProcessor(IRepository repos){
            repo = repos;
        }
        
        public Task<Player> Get(Guid id){
            return repo.GetPlayer(id);
        }
        public Task<Player[]> GetAll(){
            return repo.GetAllPlayers();
        }
        public Task<Player> Create(NewPlayer player){
            Player plr = new Player();
            plr.Name = player.Name;
            plr.Id = Guid.NewGuid();
            plr.IsBanned = false;
            return Task.FromResult(plr);
        }
        public Task<Player> Modify(Guid id, ModifiedPlayer player){
            return repo.UpdatePlayer(id, player);
        }
        public Task<Player> Delete(Guid id){
            return repo.DeletePlayer(id);
        }
        public async Task<int> GetCommonLevel(){
            return await repo.GetCommonLevel();
        }
        public async Task<Player[]> GetPlayersWithScore(int score){
            return await repo.GetPlayersWithScore(score);
        }
        public async Task<Player> GetPlayerWithName(string name){
            return await repo.GetPlayerWithName(name);
        }
        public async Task<Player[]> FindPlayersWithItem(String itemname){
            return await repo.FindPlayersWithItem(itemname);
        }
        public async Task<Player[]> FindPlayersWithItems(int items){
            return await repo.FindPlayersWithItems(items);
        }
        public async Task<Player> SellItem(Guid id, Guid itemId, int score){
            return await repo.SellItem(id,itemId,score);
        }
        public async Task<Player[]> TopTen(){
            return await repo.TopTen();
        }
    }
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController
    {
        IRepository repo;
        public PlayersController(IRepository repos){
            repo = repos;
        }

        [HttpGetAttribute]
        [Route("{playerId}")]
        public Task<Player> Get(Guid id){
            return repo.GetPlayer(id);
        }
        [HttpGetAttribute]
        [Route("")]
        public Task<Player[]> GetAll(){
            return repo.GetAllPlayers();
        }
        [HttpPostAttribute]
        [Route("")]
        public Task<Player> Create([FromBody] NewPlayer player){
           return repo.CreatePlayer(player);
        }
        [HttpPut]
        public Task<Player> Modify(Guid id,[FromBody] ModifiedPlayer player){
           return repo.UpdatePlayer(id,player);
        }
        [HttpDelete]
        [Route("{playerId}")]
        public Task<Player> Delete(Guid id){
            return repo.DeletePlayer(id);
        }
        [HttpGet("common")]
        public async Task<int> GetCommonLevel(){
            return await repo.GetCommonLevel();
        }
        [HttpGet("score")]
        public async Task<Player[]> GetPlayersWithScore(int score){
            return await repo.GetPlayersWithScore(score);
        }
        [HttpGet("name")]
        public async Task<Player> GetPlayerWithName(string name){
            return await repo.GetPlayerWithName(name);
        }
        [HttpGet("item")]
        public async Task<Player[]> FindPlayersWithItem(String itemname){
            return await repo.FindPlayersWithItem(itemname);
        }
        [HttpGet("items")]
        public async Task<Player[]> FindPlayersWithItems(int items){
            return await repo.FindPlayersWithItems(items);
        }
        [HttpPut("sell")]
        public async Task<Player> SellItem(Guid id, Guid itemId, int score){
            return await repo.SellItem(id,itemId,score);
        }
        [HttpGet("top")]
        public async Task<Player[]> TopTen(){
            return await repo.TopTen();
        }
    }
    /* 
    public class InMemoryRepository : IRepository
    {
        List<Player> players = new List<Player>();
        public Task<Player> CreatePlayer(NewPlayer player)
        {
            Player plr = new Player();
            plr.Name = player.Name;
            players.Add(plr);
            return Task.FromResult(plr);
            //throw new NotImplementedException();
        }

        public Task<Player> DeletePlayer(Guid id)
        {
            Player plr = players.Find(x => x.Id==id);
            players.Remove(plr);
            return Task.FromResult(plr);
            //throw new NotImplementedException();
        }

        public Task<Player> GetPlayer(Guid id)
        {
            Player plr = players.Find(x => x.Id==id);
            return Task.FromResult(plr);
            //throw new NotImplementedException();
        }

        public Task<Player[]> GetAllPlayers()
        {
            Player[] playerArray = players.ToArray();
            return  Task.FromResult(playerArray);
            //throw new NotImplementedException();
        }

        public Task<Player> UpdatePlayer(Guid id, ModifiedPlayer player)
        {
            Player plr = players.Find(x => x.Id==id);
            plr.Score = player.Score;
            return Task.FromResult(plr);
            //throw new NotImplementedException();
        }
        public Task<Item> CreateItem(Guid playerId, NewItem itm)
        {
            Player plr = players.Find(x => x.Id==playerId);
            Item item = new Item();
            item.Name = itm.Name;
            plr.items.Add(item);
            return Task.FromResult(item);
            //throw new NotImplementedException();
        }

        public Task<Item> DeleteItem(Guid playerid, Guid itemId)
        {
            Player plr = players.Find(x => x.Id==playerid);
            Item itm = plr.items.Find(x => x.Id==itemId);
            plr.items.Remove(itm);
            return Task.FromResult(itm);
            //throw new NotImplementedException();
        }

        public Task<Item> GetItem(Guid playerid, Guid itemId)
        {
            Player plr = players.Find(x => x.Id==playerid);
            Item itm = plr.items.Find(x => x.Id==itemId);
            return Task.FromResult(itm);
            //throw new NotImplementedException();
        }

        public Task<Item[]> GetAllItems(Guid playerId)
        {
            Player plr = players.Find(x => x.Id==playerId);
            Item[] ItemArray = plr.items.ToArray();
            return  Task.FromResult(ItemArray);
            //throw new NotImplementedException();
        }

        public Task<Item> UpdateItem(Guid id,Guid itemId, ModifiedItem item)
        {
            Player plr = players.Find(x => x.Id==id);
            Item itm = plr.items.Find(x => x.Id==itemId);
            itm.level = item.level;
            return Task.FromResult(itm);
            //throw new NotImplementedException();
        }
    }
    */
}