using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyProject.Models
{
    public class OrderModel
    {
        public int OrderId { get; set; }
        public string PersonId { get; set; }
        public int TotalBill { get; set; }
        public int BillDue { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
    }
}