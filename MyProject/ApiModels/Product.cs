using System;
using System.Collections.Generic;

namespace IqbalElectronics.DB.Models
{
    public partial class Product
    {
        public Product()
        {
            DealerPurchases = new HashSet<DealerPurchase>();
            OrderDetails = new HashSet<OrderDetail>();
        }

        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; } 
        public string Image { get; set; }
        public int Price { get; set; }
        public string CategoryId { get; set; }
        public byte Percentage { get; set; }
        public bool Daily { get; set; }
        public bool Monthly { get; set; }
        public string CreatedBy { get; set; }
        public int MinimumInstallments { get; set; }

        public virtual Category Category { get; set; } 
        public virtual Person CreatedByNavigation { get; set; }
        public virtual ICollection<DealerPurchase> DealerPurchases { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
