using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace webapi{

    public class Player
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        public bool IsBanned { get; set; }
        public DateTime CreationTime { get; set; }
       public Player(){
           
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
    public class InMemoryRepository : IRepository
    {
        List<Player> players = new List<Player>();
        public Task<Player> Create(Player player)
        {
            players.Add(player);
            return Task.FromResult(player);
            //throw new NotImplementedException();
        }

        public Task<Player> Delete(Guid id)
        {
            Player plr = players.Find(x => x.Id==id);
            players.Remove(plr);
            return Task.FromResult(plr);
            //throw new NotImplementedException();
        }

        public Task<Player> Get(Guid id)
        {
            Player plr = players.Find(x => x.Id==id);
            return Task.FromResult(plr);
            //throw new NotImplementedException();
        }

        public Task<Player[]> GetAll()
        {
            Player[] playerArray = players.ToArray();
            return  Task.FromResult(playerArray);
            //throw new NotImplementedException();
        }

        public Task<Player> Modify(Guid id, ModifiedPlayer player)
        {
            Player plr = players.Find(x => x.Id==id);
            plr.Score = player.Score;
            return Task.FromResult(plr);
            //throw new NotImplementedException();
        }
    }
    public class PlayersProcessor
    {
        IRepository repo;
        public PlayersProcessor(IRepository repos){
            repo = repos;
        }
        
        public Task<Player> Get(Guid id){
            return repo.Get(id);
        }
        public Task<Player[]> GetAll(){
            return repo.GetAll();
        }
        public Task<Player> Create(NewPlayer player){
            Player plr = new Player();
            plr.Name = player.Name;
            plr.Id = Guid.NewGuid();
            plr.IsBanned = false;
            return Task.FromResult(plr);
        }
        public Task<Player> Modify(Guid id, ModifiedPlayer player){
            return repo.Modify(id, player);
        }
        public Task<Player> Delete(Guid id){
            return repo.Delete(id);
        }
    }
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController
    {
        List<Player> players = new List<Player>();

        public Task<Player> Get(Guid id){
            Player plr = players.Find(x => x.Id==id);
            return Task.FromResult(plr);
        }
        public Task<Player[]> GetAll(){
            Player[] playerArray = players.ToArray();
            return Task.FromResult(playerArray);
        }
        public Task<Player> Create(NewPlayer player){
            Player plr = new Player();
            plr.Name = player.Name;
            players.Add(plr);
            return Task.FromResult(plr);
        }
        public Task<Player> Modify(Guid id, ModifiedPlayer player){
            Player plr = players.Find(x => x.Id==id);
            plr.Score = player.Score;
            return Task.FromResult(plr);
        }
        public Task<Player> Delete(Guid id){
            Player plr = players.Find(x => x.Id==id);
            players.Remove(plr);
            return Task.FromResult(plr);
        }
    }
}
