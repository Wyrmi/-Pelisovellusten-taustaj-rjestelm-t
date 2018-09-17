using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace webapi.models.tehtava3
{
    [Route("api/players/{playerId}/[controller]")]
    [ApiController]
    public class ItemsController
    {
        
        IRepository repo;
        public ItemsController(IRepository repos){
            repo = repos;
        }
    
        [HttpGetAttribute]
        [Route("{itemId}")]
        public Task<Item> Get(Guid playerId, Guid itemId){
            return repo.GetItem(playerId,itemId);
        }
        [HttpGetAttribute]
        [Route("")]
        public Task<Item[]> GetAll(Guid playerId){
            return repo.GetAllItems(playerId);
        }
        [HttpPostAttribute]
        [Route("")]
        public Task<Item> Create(Guid playerId,NewItem Item){
            return repo.CreateItem(playerId, Item);
        }
        [HttpPut]
        public Task<Item> Modify(Guid playerId, Guid id, ModifiedItem Item){
            return repo.UpdateItem(playerId,id,Item);
        }
        [HttpDelete]
        [Route("{itemId}")]
        public Task<Item> Delete(Guid playerId, Guid itemId){
            return repo.DeleteItem(playerId, itemId);
        } 
    }
}