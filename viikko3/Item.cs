using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace webapi
{
    public class Item
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int level { get; set; }
        public string Type {get; set;}
        public DateTime CreationDate { get; set; }
        public Item(){
            Id = Guid.NewGuid ();
            CreationDate = DateTime.Now;
        }
    }
    public class NewItem
    {
        public string Name { get; set; }
    }
    public class ModifiedItem
    {
        public int level { get; set; }
    }
}