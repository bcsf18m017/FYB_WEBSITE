using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyProject.Models
{
    public class TransactionHistoryModel
    {
        public string TransactionHistoryId { get; set; }
        public string OrderDetailId { get; set; }
        public int AmountReceived { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public String PersonID { get; set; }
    }
}