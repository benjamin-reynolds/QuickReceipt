using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuickReceipt.Models;

namespace QuickReceipt.Repositories
{
    public class VendorRepository
    {
        public List<Vendor> List()
        {
            List<Vendor> vendors = new List<Vendor>();

            //call out to Remedy service to get vendors and map to view model

            //temporary stub pending Remedy endpoint
            vendors = new List<Vendor>() { new Vendor(), new Vendor() { Id = 1, Description = "Apple" }, new Vendor() { Id = 2, Description = "HP" }, new Vendor() { Id = 3, Description = "Dell" } };

            return vendors;
        }
    }
}