//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ECOMMERCE_2023.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    public partial class ORDER
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ORDER()
        {
            this.ORDER_DETAILS = new HashSet<ORDER_DETAILS>();
        }
    
        public int Order_id { get; set; }
        public string User_id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }
        [DisplayName("Identification")]
        public string Identification_number { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
    
        public virtual AspNetUsers AspNetUsers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ORDER_DETAILS> ORDER_DETAILS { get; set; }
    }
}
