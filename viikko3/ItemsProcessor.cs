using System;
using System.Threading.Tasks;

namespace webapi
{
    public class ItemsProcessor
    {
        IRepository repo;
        public ItemsProcessor(IRepository repos){
            repo = repos;
        }
        
        public Task<Item> Get(Guid playerid, Guid itemid){
            return repo.GetItem(playerid, itemid);
        }
        public Task<Item[]> GetAll(Guid playerid){
            return repo.GetAllItems(playerid);
        }
        public Task<Item> Create(NewItem Item){
            Item itm = new Item();
            itm.Name = Item.Name;
            itm.Id = Guid.NewGuid();
            return Task.FromResult(itm);
        }
        public Task<Item> Modify(Guid playerid, Guid itemid, ModifiedItem Item){
            return repo.UpdateItem(playerid, itemid, Item);
        }
        public Task<Item> Delete(Guid playerid, Guid itemid){
            return repo.DeleteItem(playerid, itemid);
        }
    }
}