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
    
    public partial class ORDER_DETAILS
    {
        public int Order_detais_id { get; set; }
        public int Order_id { get; set; }
        public int Product_id { get; set; }
        public int Custom { get; set; }
        public int Total { get; set; }
    
        public virtual ORDER ORDER { get; set; }
        public virtual PRODUCT PRODUCT { get; set; }
    }
}