using System;
using System.Collections.Generic;

namespace IqbalElectronics.DB.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int Id { get; set; }
        public string PersonId { get; set; }
        public int TotalBill { get; set; }
        public int BillDue { get; set; }
        public DateTime CreatedOn { get; set; }

        public virtual Person Person { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
