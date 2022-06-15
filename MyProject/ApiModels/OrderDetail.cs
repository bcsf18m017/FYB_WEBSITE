using System;
using System.Collections.Generic;

namespace IqbalElectronics.DB.Models
{
    public partial class OrderDetail
    {
        public OrderDetail()
        {
            TransactionHistories = new HashSet<TransactionHistory>();
        }

        public string Id { get; set; }
        public int OrderId { get; set; }
        public string ProductId { get; set; }
        public short ProductQuantity { get; set; }
        public int TotalAmount { get; set; }
        public int AmountDue { get; set; }
        public DateTime DueDate { get; set; }
        public int MinimumInstallment { get; set; }

        public virtual Order Order { get; set; } 
        public virtual Product Product { get; set; }
        public virtual ICollection<TransactionHistory> TransactionHistories { get; set; }
    }
}
