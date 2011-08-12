using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuickReceipt.Models;
using QuickReceipt.Repositories;

namespace QuickReceipt.Controllers
{
    public class PurchaseOrderController : Controller
    {
        PurchaseOrderRepository PurchaseOrderRepository = new PurchaseOrderRepository();
        ProfileRepository ProfileRepository = new ProfileRepository();
        VendorRepository VendorRepository = new VendorRepository();
        GroupRepository GroupRepository = new GroupRepository();

        //
        // GET: /PurchaseOrder/

        public ActionResult Index()
        {
            //view the 10 or 20 most recently added orders
            return View(PurchaseOrderRepository.List().OrderByDescending(x => x.PurchaseOrderNumberDisplay).ToList());
        }

        //
        // GET: /PurchaseOrder/Details/5

        public ActionResult Details(int id)
        {
            var po = PurchaseOrderRepository.Find(id);

            if (po == null)
            {
                RedirectToAction("Index");
            }

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

        //http://localhost/QuickReceipt/PurchaseOrder/Associate?QRCode=q8ZBf
        public ActionResult Associate(string QRCode)
        {
            //qrcode doesn't exist - create
            var existingQR = PurchaseOrderRepository.Find(QRCode);

            if (existingQR == null)
            {
                existingQR = PurchaseOrderRepository.Save(new PurchaseOrder() { QRCode = QRCode });
            }

            //qrcode exists and has associated PO - go to Details
            if (existingQR.PurchaseOrderNumber.HasValue)
            {
                return RedirectToAction("Details", "PurchaseOrder", new { id = existingQR.PurchaseOrderNumber.Value });
            }

            //qrcode exists but no PO Associated - go to Scan
            return RedirectToAction("Scan", "PurchaseOrder", new { QRCode = QRCode });
        }

        //http://localhost/QuickReceipt/PurchaseOrder/Scan?QRCode=q8ZBf
        public ActionResult Scan(string QRCode)
        {
            var existingQR = PurchaseOrderRepository.Find(QRCode);
            ViewBag.Profiles = ProfileRepository.List();
            ViewBag.Vendors = VendorRepository.List();
            ViewBag.Groups = GroupRepository.List();

            return View(existingQR);
        }

        [HttpPost]
        public ActionResult Scan(PurchaseOrder po)
        {
            if (po.PurchaseOrderNumber > 0)
            {
                //associate QR Code to PO
                try
                {
                    if (ModelState.IsValid)
                    {
                        po = PurchaseOrderRepository.Save(po);

                        return RedirectToAction("Index");
                    }
                }
                catch (Exception ex)
                {
                    return View("Error", new HandleErrorInfo(ex, "PurchaseOrder", "Scan"));
                }
            }

            return View(po);
        }
    }
}
