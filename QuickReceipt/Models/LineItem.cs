using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuickReceipt.Models
{
    public class LineItem
    {
        public int Id { get; set; }

        public PurchaseOrder PurchaseOrder { get; set; }

        public string AccountingInfo { get; set; }

        public string ItemNumber { get; set; }

        public string Description { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal TotalPrice { get { return (decimal)Quantity * UnitPrice; } }

        public bool Received { get; set; }
    }
}