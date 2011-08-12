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

        public List<QuickReceipt.Models.PurchaseOrder> List()
        {
            EFModel.QuickReceiptEntities ctx = new EFModel.QuickReceiptEntities();

            var orders = (from p in ctx.PurchaseOrders
                          select p).ToList();

            return Mapper.Map(orders);
        }

        public QuickReceipt.Models.PurchaseOrder Find(string qrCode)
        {
            EFModel.QuickReceiptEntities ctx = new EFModel.QuickReceiptEntities();

            var dbPO = (from p in ctx.PurchaseOrders
                        where p.QRCodeId == qrCode
                        select p).FirstOrDefault();

            if (dbPO == null)
                return null;

            return Mapper.Map(dbPO);
        }

        public QuickReceipt.Models.PurchaseOrder Find(int purchaseOrderId)
        {
            EFModel.QuickReceiptEntities ctx = new EFModel.QuickReceiptEntities();

            var dbPO = (from p in ctx.PurchaseOrders
                        where p.PurchaseOrderId == purchaseOrderId
                        select p).FirstOrDefault();

            if (dbPO == null)
                return null;

            return Mapper.Map(dbPO);
        }

        public QuickReceipt.Models.PurchaseOrder Save(QuickReceipt.Models.PurchaseOrder po)
        {
            QuickReceiptEntities ctx = new QuickReceiptEntities();
            var dbPO = Mapper.Map(po);

            if (po.Profile != null)
            {
                dbPO.Profile = ctx.FindOrAttachEntity<EFModel.Profile, int>("Profile", "Id", po.Profile.Id);
            }

            var existingPO = (from p in ctx.PurchaseOrders
                              where p.QRCodeId == po.QRCode
                              select p).FirstOrDefault();

            if (existingPO != null)
            {
                ctx.ApplyCurrentValues<EFModel.PurchaseOrder>("PurchaseOrders", dbPO);
            }
            else
            {
                ctx.PurchaseOrders.AddObject(dbPO);
            }

            ctx.SaveChanges();

            return Mapper.Map(dbPO);
        }

        public void Delete(string QRCodeId)
        {
            EFModel.QuickReceiptEntities ctx = new EFModel.QuickReceiptEntities();

            var existingPO = (from p in ctx.PurchaseOrders
                              where p.QRCodeId == QRCodeId
                              select p).FirstOrDefault();

            if (existingPO == null)
            {
                throw new ObjectNotFoundException("Purchase Order with QR Code " + QRCodeId + "couldn't be found int the database.  No deletion was performed.");
            }

            ctx.DeleteObject(existingPO);
            ctx.SaveChanges();
        }
    }
}