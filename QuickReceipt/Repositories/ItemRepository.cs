using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuickReceipt.Models;

namespace QuickReceipt.Repositories
{
    public class ItemRepository
    {
        public List<Item> List()
        {
            List<Item> items = new List<Item>();

            //call out to Remedy service to get items and map to view model

            //temporary stub pending Remedy endpoint
            items = new List<Item>() { new Item(), new Item() { Id = 3, Description = "Laptop - existing user" }, new Item() { Id = 1, Description = "Blackberry" }, new Item() { Id = 2, Description = "iDevice" } };

            return items;
        }
    }
}