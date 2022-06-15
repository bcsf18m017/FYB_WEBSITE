using System;
using System.Collections.Generic;

namespace IqbalElectronics.DB.Models
{
    public partial class PersonGuarantee
    {
        public string Id { get; set; }
        public string PersonId { get; set; }
        public string GuaranteeId { get; set; }
        public string CreatorId { get; set; }
        public DateTime CreatedOn { get; set; }
        
        public virtual Person Creator { get; set; } 
        public virtual Person Guarantee { get; set; }
        public virtual Person Person { get; set; } 
    }
}
