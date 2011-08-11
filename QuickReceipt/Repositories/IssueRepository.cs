using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuickReceipt.Models;

namespace QuickReceipt.Repositories
{
    public class IssueRepository
    {
        public List<Issue> List()
        {
            List<Issue> issues = new List<Issue>();

            //call out to Remedy service to get items and map to view model

            //temporary stub pending Remedy endpoint
            issues = new List<Issue>() { new Issue(), new Issue() { Id = 1, Description = "Install Hardware" }, new Issue() { Id = 2, Description = "Another Issue" } };

            return issues;
        }
    }
}