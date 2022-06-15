using System;
using System.Collections.Generic;

namespace IqbalElectronics.DB.Models
{
    public partial class TransactionHistory
    {
        public string TransactionHistoryId { get; set; }
        public string OrderDetailId { get; set; }
        public int AmountReceived { get; set; }
        public DateTime CreatedOn { get; set; }

        public virtual OrderDetail OrderDetail { get; set; } 
    }
}
