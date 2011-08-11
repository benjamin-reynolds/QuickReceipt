using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuickReceipt.Models;

namespace QuickReceipt.Repositories
{
    public class CategoryRepository
    {
        public List<Category> List()
        {
            List<Category> categories = new List<Category>();

            //call out to Remedy service to get Categories and map to view model

            categories = new List<Category>() { new Category(), new Category() { Id = 1, Description = "Hardware User", }, new Category() { Id = 2, Description = "Another Category" } };

            return categories;
        }
    }
}