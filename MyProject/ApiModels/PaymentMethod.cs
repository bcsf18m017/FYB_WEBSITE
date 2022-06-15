using System;
using System.Collections.Generic;

namespace IqbalElectronics.DB.Models
{
    public partial class PaymentMethod
    {
        public PaymentMethod()
        {
            DealerPurchases = new HashSet<DealerPurchase>();
        }

        public string Id { get; set; }
        public string Description { get; set; } 

        public virtual ICollection<DealerPurchase> DealerPurchases { get; set; }
    }
}
