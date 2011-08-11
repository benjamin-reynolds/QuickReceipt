using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuickReceipt.Models;

namespace QuickReceipt.Repositories
{
    public class GroupRepository
    {
        public List<Group> List()
        {
            List<Group> groups = new List<Group>();

            //call out to Remedy service to get groups and map to view model

            //temporary stub pending Remedy endpoint
            groups = new List<Group>() { new Group(), new Group() { Id = 1, Description = "IMAC" }, new Group() { Id = 2, Description = "Some Other Group" } };

            return groups;
        }
    }
}