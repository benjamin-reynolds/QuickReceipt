using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuickReceipt.Models;
using QuickReceipt.Repositories;

namespace QuickReceipt.Mappers
{
    public class PurchaseOrderMapper
    {
        ProfileRepository ProfileRepository = new ProfileRepository();
        GroupRepository GroupRepository = new GroupRepository();
        VendorRepository VendorRepository = new VendorRepository();

        public List<EFModel.PurchaseOrder> Map(List<PurchaseOrder> pos)
        {
            List<EFModel.PurchaseOrder> dbPos = new List<EFModel.PurchaseOrder>();
            foreach (var p in pos)
            {
                dbPos.Add(Map(p));
            }

            return dbPos;
        }

        public List<PurchaseOrder> Map(List<EFModel.PurchaseOrder> pos)
        {
            List<PurchaseOrder> dbPos = new List<PurchaseOrder>();
            foreach (var p in pos)
            {
                dbPos.Add(Map(p));
            }

            return dbPos;
        }

        public EFModel.PurchaseOrder Map(PurchaseOrder p)
        {
            EFModel.PurchaseOrder po = new EFModel.PurchaseOrder();
            po.QRCodeId = p.QRCode;
            po.PurchaseOrderId = p.PurchaseOrderNumber;
            po.GroupQRCode = p.GroupQRCode;
            po.ProfileId = (p.Profile == null ? null : (int?)p.Profile.Id);
            po.GroupId = (p.Group == null ? null : (int?)p.Group.Id);
            po.VendorId = (p.Vendor == null ? null : (int?)p.Vendor.Id);

            return po;
        }

        public PurchaseOrder Map(EFModel.PurchaseOrder p)
        {
            PurchaseOrder po = new PurchaseOrder();
            po.QRCode = p.QRCodeId;
            po.PurchaseOrderNumber = p.PurchaseOrderId;
            po.GroupQRCode = p.GroupQRCode;
            po.Profile = (p.ProfileId.HasValue ? ProfileRepository.List().Where(x => x.Id == p.ProfileId.Value).First() : null);
            po.Group = (p.GroupId.HasValue ? GroupRepository.List().Where(x => x.Id == p.GroupId.Value).First() : null);
            po.Vendor = (p.VendorId.HasValue ? VendorRepository.List().Where(x => x.Id == p.VendorId.Value).First() : null);

            return po;
        }

    }
}