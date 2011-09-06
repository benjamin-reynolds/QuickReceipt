using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuickReceipt.Models;
using QuickReceipt.Repositories;
using System.Net;
using System.IO;
using System.Text;

namespace QuickReceipt.Controllers
{
    public class QRCodeController : Controller
    {
        // GET: /QRCode/
        public ActionResult Index()
        {
            PurchaseOrderRepository rep = new PurchaseOrderRepository();
            List<string> urls = rep.ListUnusedQRUrls(18);
            ViewBag.UnusedQRCodeCount = rep.GetUnusedQRCodeCount();
            return View(urls);
        }

        public ActionResult PrinterFriendlyIndex(int numPages)
        {
            PurchaseOrderRepository rep = new PurchaseOrderRepository();
            List<string> urls = rep.ListUnusedQRUrls(48 * numPages);
            return View(urls);
        }

        // GET: /QRCode/Build
        public ActionResult Build(int numToBuild)
        {
            PurchaseOrderRepository rep = new PurchaseOrderRepository();
            List<string> shortURLs = new List<string>();

            for (int i = 0; i < numToBuild; i++)
            {
                PurchaseOrder po = new PurchaseOrder();
                po = rep.Save(po);

                string buildURL = "http://qr.tbs.io/qr/build?entityid=14&poid=" + po.POId.ToString() + "&url=true";
                string buildResponse = CallURL(buildURL);
                string qrCodeShortURL = buildResponse.Substring(buildResponse.LastIndexOf('?') + 3);
                qrCodeShortURL = qrCodeShortURL.Remove(qrCodeShortURL.LastIndexOf('"'));
                po.QRCodeShortURL = qrCodeShortURL;

                po = rep.Save(po);
                shortURLs.Add(po.QRCodeShortURL);
            }
            
            return RedirectToAction("Index");
        }

        #region WebHelpers

        private string CallURL(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream respStream = response.GetResponseStream();
            StringBuilder sb = new StringBuilder();
            byte[] buf = new byte[8192];
            string temp = null;
            int count = 0;
            do
            {
                count = respStream.Read(buf, 0, buf.Length);
                if (count != 0)
                {
                    temp = Encoding.ASCII.GetString(buf, 0, count);
                    sb.Append(temp);
                }
            }
            while (count > 0);

            return sb.ToString();
        }

        #endregion
    }
}
