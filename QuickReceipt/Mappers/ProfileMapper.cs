using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuickReceipt.Models;
using QuickReceipt.Repositories;

namespace QuickReceipt.Mappers
{
    public class ProfileMapper
    {
        CategoryRepository CategoryRepository = new CategoryRepository();
        TypeRepository TypeRepository = new TypeRepository();
        ItemRepository ItemRepository = new ItemRepository();
        IssueRepository IssueRepository = new IssueRepository();

        public List<EFModel.Profile> Map(List<Profile> profiles)
        {
            List<EFModel.Profile> profs = new List<EFModel.Profile>();
            foreach (var p in profiles)
            {
                profs.Add(Map(p));
            }

            return profs;
        }

        public List<Profile> Map(List<EFModel.Profile> profiles)
        {
            List<Profile> profs = new List<Profile>();
            foreach (var p in profiles)
            {
                profs.Add(Map(p));
            }

            return profs;
        }

        public EFModel.Profile Map(Profile p)
        {
            EFModel.Profile prof = new EFModel.Profile();
            prof.Id = p.Id;
            prof.Name = p.Name;
            prof.CategoryId = p.Category.Id;
            prof.IssueId = p.Issue.Id;
            prof.ItemId = p.Item.Id;
            prof.TypeId = p.Type.Id;

            return prof;
        }

        public Profile Map(EFModel.Profile p)
        {
            Profile prof = new Profile();
            prof.Id = p.Id;
            prof.Name = p.Name;
            prof.Category = CategoryRepository.List().Where(x => x.Id == p.CategoryId).First();
            prof.Issue = IssueRepository.List().Where(x => x.Id == p.IssueId).First();
            prof.Item = ItemRepository.List().Where(x => x.Id == p.ItemId).First();
            prof.Type = TypeRepository.List().Where(x => x.Id == p.TypeId).First();

            return prof;
        }
    }
}