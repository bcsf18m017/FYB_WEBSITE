using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IqbalElectronics.DB.Models
{
    public partial class Person
    {
        public Person()
        {
            DealerPurchases = new HashSet<DealerPurchase>();
            Orders = new HashSet<Order>();
            PersonGuaranteeGuarantees = new HashSet<PersonGuarantee>();
            PersonGuaranteePeople = new HashSet<PersonGuarantee>();
        }

        public string Name { get; set; } 
        public string Address { get; set; } 
        public string Cnic { get; set; } 
        public string Image { get; set; }
        public int? Salary { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public string CreatorId { get; set; }
        
        public virtual Person CreatedByNavigation { get; set; }
        public virtual ICollection<DealerPurchase> DealerPurchases { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<PersonGuarantee> PersonGuaranteeGuarantees { get; set; }
        public virtual ICollection<PersonGuarantee> PersonGuaranteePeople { get; set; }
    }
}
