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
    
    public partial class Images
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Images()
        {
            this.Pl_Halls = new HashSet<Pl_Halls>();
        }
    
        public int Id { get; set; }
        public int Pl_Halls_Id { get; set; }
        public string Image_Url { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Pl_Halls> Pl_Halls { get; set; }
    }
}
