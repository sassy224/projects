//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VCCI.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Department
    {
        public Department()
        {
            this.Quotas = new HashSet<Quota>();
            this.Quotas1 = new HashSet<Quota>();
        }
    
        public decimal ID { get; set; }
        public string Name { get; set; }
    
        public virtual ICollection<Quota> Quotas { get; set; }
        public virtual ICollection<Quota> Quotas1 { get; set; }
    }
}
