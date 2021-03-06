using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;

namespace webapi
{
    public class MongoDbRepository : IRepository
    {
        private readonly IMongoCollection<Player> _collection;
        private readonly IMongoCollection<BsonDocument> _bsonDocumentCollection;
        List<Player> players = new List<Player>();
        public MongoDbRepository()
        {
            var mongoClient = new MongoClient("mongodb://localhost:27017");
            IMongoDatabase database = mongoClient.GetDatabase("Game");
            _collection = database.GetCollection<Player>("players");
            _bsonDocumentCollection = database.GetCollection<BsonDocument>("players");
        }
        public async Task<Player> CreatePlayer(NewPlayer player)
        {
            Player plr = new Player();
            plr.Name = player.Name;
            await _collection.InsertOneAsync(plr);
            return plr;
        }

        public async Task<Player> DeletePlayer(Guid id)
        {
            FilterDefinition<Player> filter = Builders<Player>.Filter.Eq("_id", id);
            return await _collection.FindOneAndDeleteAsync(filter);
        }

        public Task<Player> GetPlayer(Guid id)
        {
            FilterDefinition<Player> filter = Builders<Player>.Filter.Eq("_id", id);
            return _collection.Find(filter).FirstAsync();
        }

        public async Task<Player[]> GetAllPlayers()
        {
            List<Player> players = await _collection.Find(new BsonDocument()).ToListAsync();
            return players.ToArray();
        }

        public async Task<Player> UpdatePlayer(Guid id, ModifiedPlayer player)
        {
            var filter = Builders<Player>.Filter.Eq("_id", id);
            var update = Builders<Player>.Update.Set("Score",player.Score);
            return await _collection.FindOneAndUpdateAsync(filter, update);;
        }
        public async Task<Player[]> GetAllSortedByScoreDescending()
        {
            SortDefinition<Player> sortDef = Builders<Player>.Sort.Descending(p => p.Score);
            List<Player> players = await _collection.Find(new BsonDocument()).Sort(sortDef).ToListAsync();
            return players.ToArray();
        }

        public async Task<Player> IncrementPlayerScore(string id, int increment)
        {
            var filter = Builders<Player>.Filter.Eq("_id", id);
            var incrementScoreUpdate = Builders<Player>.Update.Inc(p => p.Score, increment);
            var options = new FindOneAndUpdateOptions<Player>()
            {
                ReturnDocument = ReturnDocument.After
            };
            Player player = await _collection.FindOneAndUpdateAsync(filter, incrementScoreUpdate, options);
            return player;
        }
        public Task<Player> IncreasePlayerScoreAndRemoveItem(Guid playerId, Guid itemId, int score)
        {
            var pull = Builders<Player>.Update.PullFilter(p => p.items, i => i.Id == itemId);
            var inc = Builders<Player>.Update.Inc(p => p.Score, score);
            var update = Builders<Player>.Update.Combine(pull, inc);
            var filter = Builders<Player>.Filter.Eq(p => p.Id, playerId);

            return _collection.FindOneAndUpdateAsync(filter, update);
        }
        public async Task<Item> CreateItem(Guid playerId, NewItem itm)
        {
            FilterDefinition<Player> filter = Builders<Player>.Filter.Eq("_id", playerId);
            Player plr = await _collection.Find(filter).FirstAsync();
            Item item = new Item();
            item.Name = itm.Name;
            plr.items.Add(item);
            await _collection.ReplaceOneAsync(filter, plr);
            return item;
        }

        public async Task<Item> DeleteItem(Guid playerid, Guid itemId)
        {
            FilterDefinition<Player> filter = Builders<Player>.Filter.Eq("_id", playerid);
            Player plr = await _collection.Find(filter).FirstAsync();
            Item itm = plr.items.Find(x => x.Id==itemId);
            plr.items.Remove(itm);
            return itm;
        }

        public async Task<Item> GetItem(Guid playerid, Guid itemId)
        {
            FilterDefinition<Player> filter = Builders<Player>.Filter.Eq("_id", playerid);
            Player plr = await _collection.Find(filter).FirstAsync();
            Item itm = plr.items.Find(x => x.Id==itemId);
            return itm;
        }

        public async Task<Item[]> GetAllItems(Guid playerId)
        {
            FilterDefinition<Player> filter = Builders<Player>.Filter.Eq("_id", playerId);
            Player plr = await _collection.Find(filter).FirstAsync();
            Item[] ItemArray = plr.items.ToArray();
            return  ItemArray;
        }

        public async Task<Item> UpdateItem(Guid id,Guid itemId, ModifiedItem item)
        {
            FilterDefinition<Player> filter = Builders<Player>.Filter.Eq("_id", id);
            Player plr = await _collection.Find(filter).FirstAsync();
            Item itm = plr.items.Find(x => x.Id==itemId);
            itm.level = item.level;
            return itm;
        }
    }
}
