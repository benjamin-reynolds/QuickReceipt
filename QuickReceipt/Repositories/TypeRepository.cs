using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuickReceipt.Models;

namespace QuickReceipt.Repositories
{
    public class TypeRepository
    {
        public List<ProfileType> List()
        {
            List<ProfileType> types = new List<ProfileType>();

            //call out to Remedy service to get types and map to view model

            //temporary stub pending Remedy endpoint
            types = new List<ProfileType>() { new ProfileType(), new ProfileType() { Id = 2, Description = "Laptop" }, new ProfileType() { Id = 1, Description = "Mobile Device" } };

            return types;
        }
    }
}