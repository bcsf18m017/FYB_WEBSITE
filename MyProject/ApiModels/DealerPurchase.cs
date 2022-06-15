using System;
using System.Collections.Generic;

namespace IqbalElectronics.DB.Models
{
    public partial class DealerPurchase
    {
        public int Id { get; set; }
        public string PersonId { get; set; }
        public string ProductId { get; set; }
        public short Quantity { get; set; }
        public string PaymentMethodId { get; set; }
        public int TotalBill { get; set; }
        public DateTime CreatedOn{ get; set; }

        public virtual PaymentMethod PaymentMethod { get; set; }
        public virtual Person Person { get; set; }
        public virtual Product Product { get; set; }
    }
}
