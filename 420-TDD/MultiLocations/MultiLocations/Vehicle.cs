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
    
    public partial class Vehicle
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Vehicle()
        {
            this.Locations = new HashSet<Location>();
        }
    
        public string VINVehicule { get; set; }
        public string Modele { get; set; }
        public string Type { get; set; }
        public string Couleur { get; set; }
        public int Annee { get; set; }
        public int ValeurVehicule { get; set; }
        public byte TransmissionAuto { get; set; }
        public byte AirClim { get; set; }
        public byte AntiDemarreur { get; set; }
    
        public virtual Couleur Couleur1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Location> Locations { get; set; }
        public virtual Modele Modele1 { get; set; }
        public virtual Type Type1 { get; set; }
    }
}
