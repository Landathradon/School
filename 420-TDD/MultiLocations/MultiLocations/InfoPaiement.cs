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
    
    public partial class InfoPaiement
    {
        public int NoPaiement { get; set; }
        public string NoLocation { get; set; }
        public DateTime? Date { get; set; }
        public decimal Montant { get; set; }
        public string LocationPaiement { get; set; }
    
        public virtual Location Location { get; set; }
    }
}
