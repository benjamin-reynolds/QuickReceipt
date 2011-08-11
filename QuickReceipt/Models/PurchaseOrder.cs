using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuickReceipt.Models
{
    public class PurchaseOrder
    {
        public int Id { get; set; }

        public int PurchaseOrderNumber { get; set; }

        public string PurchaseOrderNumberDisplay 
        { 
            get 
            { 
                return (PurchaseOrderNumber == 0 ? "" : PurchaseOrderNumber.ToString()); 
            }
            set
            {
                PurchaseOrderNumber = Convert.ToInt32(value);
            }
        }

        public DateTime PODate { get; set; }

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
    }
}