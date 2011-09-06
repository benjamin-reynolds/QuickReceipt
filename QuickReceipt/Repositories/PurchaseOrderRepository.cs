using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuickReceipt.Models;
using QuickReceipt.Mappers;
using System.Data;
using EFModel;

namespace QuickReceipt.Repositories
{
    public class PurchaseOrderRepository
    {
        PurchaseOrderMapper Mapper = new PurchaseOrderMapper();

        public List<string> ListUnusedQRUrls(int numToList)
        {
            QuickReceiptEntities ctx = new QuickReceiptEntities();

            var urls = (from p in ctx.PurchaseOrders
                        where p.PurchaseOrderNumber == null && p.QRCodeShortURL != null
                        select p.QRCodeShortURL).Take(numToList).ToList();

            return urls;
        }

        public int GetUnusedQRCodeCount()
        {
            QuickReceiptEntities ctx = new QuickReceiptEntities();

            var count = (from p in ctx.PurchaseOrders
                        where p.PurchaseOrderNumber == null && p.QRCodeShortURL != null
                        select p.QRCodeShortURL).Count();

            return count;
        }

        public List<QuickReceipt.Models.PurchaseOrder> List(int numToList)
        {
            QuickReceiptEntities ctx = new QuickReceiptEntities();

            var orders = (from p in ctx.PurchaseOrders
                          where p.PurchaseOrderNumber.HasValue
                          select p).Take(numToList).ToList();

            return Mapper.Map(orders);
        }

        public QuickReceipt.Models.PurchaseOrder Find(string qrCode)
        {
            QuickReceiptEntities ctx = new QuickReceiptEntities();

            var dbPO = (from p in ctx.PurchaseOrders
                        where p.QRCodeId == qrCode
                        select p).FirstOrDefault();

            if (dbPO == null)
                return null;

            return Mapper.Map(dbPO);
        }

        public QuickReceipt.Models.PurchaseOrder Find(int poId)
        {
            QuickReceiptEntities ctx = new QuickReceiptEntities();

            var dbPO = (from p in ctx.PurchaseOrders
                        where p.POId == poId
                        select p).FirstOrDefault();

            if (dbPO == null)
                return null;

            return Mapper.Map(dbPO);
        }

        public QuickReceipt.Models.PurchaseOrder Save(QuickReceipt.Models.PurchaseOrder po)
        {
            QuickReceiptEntities ctx = new QuickReceiptEntities();
            var dbPO = Mapper.Map(po);

            var existingPO = (from p in ctx.PurchaseOrders
                              where p.POId == po.POId
                              select p).FirstOrDefault();

            if (existingPO != null)
            {
                existingPO.ProfileId = (po.ProfileId == 0 ? null : (int?)po.ProfileId);
                existingPO.GroupId = (po.GroupId == 0 ? null : (int?)po.GroupId);
                existingPO.GroupQRCode = po.GroupQRCode;
                existingPO.PurchaseOrderNumber = po.PurchaseOrderNumber;
                existingPO.QRCodeId = po.QRCodeId;
                existingPO.VendorId = (po.VendorId == 0 ? null : (int?)po.VendorId);
                existingPO.QRCodeShortURL = po.QRCodeShortURL;
            }
            else
            {
                ctx.PurchaseOrders.AddObject(dbPO);
            }

            ctx.SaveChanges();

            return Mapper.Map(dbPO);
        }

        public void Delete(int POId)
        {
            QuickReceiptEntities ctx = new QuickReceiptEntities();

            var existingPO = (from p in ctx.PurchaseOrders
                              where p.POId == POId
                              select p).FirstOrDefault();

            if (existingPO == null)
            {
                throw new ObjectNotFoundException("Purchase Order with POId " + POId + "couldn't be found int the database.  No deletion was performed.");
            }

            ctx.DeleteObject(existingPO);
            ctx.SaveChanges();
        }
    }
}