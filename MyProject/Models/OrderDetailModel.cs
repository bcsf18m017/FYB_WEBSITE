using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyProject.Models
{
    public class OrderDetailModel
    {
        public string OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public string ProductId { get; set; }
        public short ProductQuantity { get; set; }
        public int TotalAmount { get; set; }
        public int AmountDue { get; set; }
        public DateTime DueDate { get; set; } = DateTime.Now;
        public int MinimumInstallment { get; set; }

    }
}