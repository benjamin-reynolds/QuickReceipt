using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuickReceipt.Models;
using QuickReceipt.Repositories;

namespace QuickReceipt.Controllers
{
    public class ProfileController : Controller
    {
        ProfileRepository ProfileRepository = new ProfileRepository();
        CategoryRepository CategoryRepository = new CategoryRepository();
        TypeRepository TypeRepository = new TypeRepository();
        ItemRepository ItemRepository = new ItemRepository();
        IssueRepository IssueRepository = new IssueRepository();

        //
        // GET: /Profile/

        public ActionResult Index()
        {
            var profiles = ProfileRepository.List();
            var sortedProfiles = profiles.OrderBy(x => x.Name).ToList();

            return View(sortedProfiles);
        }

        //
        // GET: /Profile/Create

        public ActionResult Create()
        {
            ViewBag.Categories = CategoryRepository.List();
            ViewBag.Types = TypeRepository.List();
            ViewBag.Items = ItemRepository.List();
            ViewBag.Issues = IssueRepository.List();

            Profile p = new Profile();
            return View(p);
        } 

        //
        // POST: /Profile/Create

        [HttpPost]
        public ActionResult Create(Profile profile)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    profile = ProfileRepository.Save(profile);
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("Name", ex);
            }

            if (profile.Id != 0)
            {
                return RedirectToAction("Index");
            }

            return View(profile);
        }
        
        //
        // GET: /Profile/Edit/5
 
        public ActionResult Edit(int id)
        {
            ViewBag.Categories = CategoryRepository.List();
            ViewBag.Types = TypeRepository.List();
            ViewBag.Items = ItemRepository.List();
            ViewBag.Issues = IssueRepository.List();

            var profile = (from p in ProfileRepository.List()
                           where p.Id == id
                           select p).FirstOrDefault();

            return View(profile);
        }

        //
        // POST: /Profile/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, Profile profile)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    profile = ProfileRepository.Save(profile);

                    return RedirectToAction("Index");
                }
            }
            catch(Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Profile", "Edit"));
            }

            return View(profile);
        }

        //
        // GET: /Profile/Delete/5
 
        public ActionResult Delete(int id)
        {
            try
            {
                ProfileRepository.Delete(id);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Profile", "Delete"));
            }

            return RedirectToAction("Index");
        }
    }
}
