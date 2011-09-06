using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace QuickReceipt.Models
{
    public class PurchaseOrder
    {
        public int POId { get; set; }

        public string QRCodeId { get; set; }

        public string QRCodeShortURL { get; set; }

        [Required(ErrorMessage="You must associate a Purchase Order Number")]
        public int? PurchaseOrderNumber { get; set; }

        public bool GroupQRCode { get; set; }

        public string PurchaseOrderNumberDisplay 
        { 
            get 
            {
                return (PurchaseOrderNumber.HasValue ? PurchaseOrderNumber.Value.ToString() : ""); 
            }
            set
            {
                PurchaseOrderNumber = Convert.ToInt32(value);
            }
        }

        public DateTime? PODate { get; set; }

        public string PaymentTerms { get; set; }

        public string FreightTerms { get; set; }

        public string ShipVia { get; set; }

        public string TrackingNumber { get; set; }

        public List<LineItem> LineItems { get; set; }

        public decimal Subtotal
        {
            get
            {
                return LineItems == null ? 0 : LineItems.Sum(x => x.TotalPrice);
            }
        }

        public decimal Tax { get; set; }

        public decimal Total
        {
            get
            {
                return Subtotal + Tax;
            }
        }

        public string WorkOrderText { get; set; }

        public Profile Profile { get; set; }

        public int ProfileId
        {
            get
            {
                return (Profile == null ? 0 : Profile.Id);
            }
            set
            {
                if (Profile == null)
                    Profile = new Profile();

                Profile.Id = value;
            }
        }

        public Vendor Vendor { get; set; }

        public int VendorId
        {
            get
            {
                return (Vendor == null ? 0 : Vendor.Id);
            }
            set
            {
                if (Vendor == null)
                    Vendor = new Vendor();

                Vendor.Id = value;
            }
        }

        public Group Group { get; set; }

        public int GroupId
        {
            get
            {
                return (Group == null ? 0 : Group.Id);
            }
            set
            {
                if (Group == null)
                    Group = new Group();

                Group.Id = value;
            }
        }
    }
}