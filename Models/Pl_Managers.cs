//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SarayTap.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Pl_Managers
    {
        public int Id { get; set; }
        public int Palace_Id { get; set; }
        public int Manager_Id { get; set; }
    
        public virtual Managers Managers { get; set; }
        public virtual Palaces Palaces { get; set; }
    }
}
