//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MultiLocations
{
    using System;
    using System.Collections.Generic;
    
    public partial class TermesDeLocation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TermesDeLocation()
        {
            this.Locations = new HashSet<Location>();
        }
    
        public string NoTerme { get; set; }
        public byte NbrAnnees { get; set; }
        public int KiloMax { get; set; }
        public decimal Prime { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Location> Locations { get; set; }
    }
}