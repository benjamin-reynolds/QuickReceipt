using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuickReceipt.Models;

namespace QuickReceipt.Controllers
{
    public class PurchaseOrderController : Controller
    {
        List<PurchaseOrder> purchaseOrders = new List<PurchaseOrder>()
            //{
            //    new PurchaseOrder() { Id = 1, PurchaseOrderNumber = 105809, PODate = new DateTime(2011, 7, 12), PaymentTerms = "Net 30", FreightTerms = "Origin", TrackingNumber = "TTS000003501097", ShipVia = "2nd Day", LineItems = new List<LineItem>() },
            //    new PurchaseOrder() { Id = 2, PurchaseOrderNumber = 105923, PODate = new DateTime(2011, 7, 15), PaymentTerms = "Net 30", FreightTerms = "Origin", TrackingNumber = "TTS000003501233", ShipVia = "Next Day Air", LineItems = new List<LineItem>() },
            //    new PurchaseOrder() { Id = 2, PurchaseOrderNumber = 105966, PODate = new DateTime(2011, 7, 16), PaymentTerms = "Net 30", FreightTerms = "Bulk", TrackingNumber = "TTS000003501256", ShipVia = "Ground", LineItems = new List<LineItem>() },
            //}
            ;

        private void SetUpTestData()
        {
            purchaseOrders = new List<PurchaseOrder>();

            var po = new PurchaseOrder() { Id = 1, PurchaseOrderNumber = 105809, PODate = new DateTime(2011, 7, 12), PaymentTerms = "Net 30", FreightTerms = "Origin", TrackingNumber = "TTS000003501097", ShipVia = "2nd Day", LineItems = new List<LineItem>() };
            var li = new LineItem() { Id = 1, AccountingInfo = "1234 / 123412344 / 123123", ItemNumber = "asdf12343l", Description = "Some electronics!", Quantity = 2, UnitPrice = 55.34M, PurchaseOrder = po, Received = false };
            po.LineItems.Add(li);
            purchaseOrders.Add(po);

            po = new PurchaseOrder() { Id = 2, PurchaseOrderNumber = 105923, PODate = new DateTime(2011, 7, 15), PaymentTerms = "Net 30", FreightTerms = "Origin", TrackingNumber = "TTS000003501233", ShipVia = "Next Day Air", LineItems = new List<LineItem>() };
            li = new LineItem() { Id = 2, AccountingInfo = "1234 / 123412344 / 123123", ItemNumber = "asdf12343l", Description = "Some electronics!", Quantity = 2, UnitPrice = 55.34M, PurchaseOrder = po, Received = true };
            po.LineItems.Add(li);
            li = new LineItem() { Id = 3, AccountingInfo = "1234 / 123412344 / 123123", ItemNumber = "asdf12343l", Description = "More electronics!", Quantity = 1, UnitPrice = 12.09M, PurchaseOrder = po, Received = false };
            po.LineItems.Add(li);
            purchaseOrders.Add(po);

            po = new PurchaseOrder() { Id = 2, PurchaseOrderNumber = 105966, PODate = new DateTime(2011, 7, 16), PaymentTerms = "Net 30", FreightTerms = "Bulk", TrackingNumber = "TTS000003501256", ShipVia = "Ground", LineItems = new List<LineItem>() };
            li = new LineItem() { Id = 4, AccountingInfo = "1234 / 123412344 / 123123", ItemNumber = "asdf12343l", Description = "Some electronics!", Quantity = 2, UnitPrice = 55.34M, PurchaseOrder = po, Received = true };
            po.LineItems.Add(li);
            li = new LineItem() { Id = 5, AccountingInfo = "1234 / 123412344 / 123123", ItemNumber = "asdf12343l", Description = "More electronics!", Quantity = 1, UnitPrice = 12.09M, PurchaseOrder = po, Received = false };
            po.LineItems.Add(li);
            li = new LineItem() { Id = 6, AccountingInfo = "1234 / 123412344 / 123123", ItemNumber = "asdf12343l", Description = "Best electronics!", Quantity = 100, UnitPrice = 0.89M, PurchaseOrder = po, Received = true };
            po.LineItems.Add(li);
            li = new LineItem() { Id = 7, AccountingInfo = "1234 / 123412344 / 123123", ItemNumber = "asdf12343l", Description = "Worst electronics!", Quantity = 12, UnitPrice = 0.01M, PurchaseOrder = po, Received = false };
            po.LineItems.Add(li);
            purchaseOrders.Add(po);
        }

        //
        // GET: /PurchaseOrder/

        public ActionResult Index()
        {
            SetUpTestData();
            List<PurchaseOrder> sortedOrders = purchaseOrders.OrderByDescending(x => x.Id).ToList();

            //view the 10 or 20 most recently added orders
            return View(sortedOrders);
        }

        //
        // GET: /PurchaseOrder/Details/5

        public ActionResult Details(int id)
        {
            SetUpTestData();
            var po = (from p in purchaseOrders
                      where p.PurchaseOrderNumber == id
                      select p).FirstOrDefault();

            return View(po);
        }

        [HttpPost]
        public ActionResult Details([Bind(Include="LineItems")] int id, PurchaseOrder po)
        {
            try
            {
                if (po.Id != 0)
                {
                    return RedirectToAction("Index");
                }

                return View(po);
            }
            catch
            {
                return View(po);
            }
        }

        public ActionResult Scan()
        {
            List<Profile> profiles = new List<Profile>();
            profiles.Add(new Profile());
            profiles.Add(new Profile() { Id = 1, Name = "Laptop" });
            profiles.Add(new Profile() { Id = 2, Name = "Blackberry" });
            profiles.Add(new Profile() { Id = 3, Name = "iPhone" });
            ViewBag.Profiles = profiles;

            PurchaseOrder po = new PurchaseOrder();
            return View(po);
        }

        [HttpPost]
        public ActionResult Scan(PurchaseOrder po)
        {
            if (po.PurchaseOrderNumber > 0)
            {
                //associate QR Code to PO
                return RedirectToAction("Index");
            }

            return View(po);
        }
    }
}
