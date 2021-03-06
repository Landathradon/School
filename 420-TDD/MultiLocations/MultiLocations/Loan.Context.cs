﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class LoanEntities : DbContext
    {
        public LoanEntities()
            : base("name=LoanEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Audit> Audits { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Couleur> Couleurs { get; set; }
        public virtual DbSet<InfoPaiement> InfoPaiements { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Modele> Modeles { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<TermesDeLocation> TermesDeLocations { get; set; }
        public virtual DbSet<Type> Types { get; set; }
        public virtual DbSet<Vehicle> Vehicles { get; set; }
    
        public virtual int CreateLeaseProcedure(string noLocation, Nullable<System.DateTime> dateLocation, Nullable<System.DateTime> datePaiement, Nullable<decimal> montantPaiement, Nullable<int> nbrPaiement, string vINVehicule, string noClient, Nullable<byte> nbrAnnees, Nullable<int> kiloMax, Nullable<decimal> prime)
        {
            var noLocationParameter = noLocation != null ?
                new ObjectParameter("NoLocation", noLocation) :
                new ObjectParameter("NoLocation", typeof(string));
    
            var dateLocationParameter = dateLocation.HasValue ?
                new ObjectParameter("DateLocation", dateLocation) :
                new ObjectParameter("DateLocation", typeof(System.DateTime));
    
            var datePaiementParameter = datePaiement.HasValue ?
                new ObjectParameter("DatePaiement", datePaiement) :
                new ObjectParameter("DatePaiement", typeof(System.DateTime));
    
            var montantPaiementParameter = montantPaiement.HasValue ?
                new ObjectParameter("MontantPaiement", montantPaiement) :
                new ObjectParameter("MontantPaiement", typeof(decimal));
    
            var nbrPaiementParameter = nbrPaiement.HasValue ?
                new ObjectParameter("NbrPaiement", nbrPaiement) :
                new ObjectParameter("NbrPaiement", typeof(int));
    
            var vINVehiculeParameter = vINVehicule != null ?
                new ObjectParameter("VINVehicule", vINVehicule) :
                new ObjectParameter("VINVehicule", typeof(string));
    
            var noClientParameter = noClient != null ?
                new ObjectParameter("NoClient", noClient) :
                new ObjectParameter("NoClient", typeof(string));
    
            var nbrAnneesParameter = nbrAnnees.HasValue ?
                new ObjectParameter("NbrAnnees", nbrAnnees) :
                new ObjectParameter("NbrAnnees", typeof(byte));
    
            var kiloMaxParameter = kiloMax.HasValue ?
                new ObjectParameter("KiloMax", kiloMax) :
                new ObjectParameter("KiloMax", typeof(int));
    
            var primeParameter = prime.HasValue ?
                new ObjectParameter("Prime", prime) :
                new ObjectParameter("Prime", typeof(decimal));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("CreateLeaseProcedure", noLocationParameter, dateLocationParameter, datePaiementParameter, montantPaiementParameter, nbrPaiementParameter, vINVehiculeParameter, noClientParameter, nbrAnneesParameter, kiloMaxParameter, primeParameter);
        }
    
        public virtual int InsertPaiementsProcedure(Nullable<int> noPaiement, string noLocation, Nullable<System.DateTime> date, Nullable<decimal> montant, string locationPaiement)
        {
            var noPaiementParameter = noPaiement.HasValue ?
                new ObjectParameter("NoPaiement", noPaiement) :
                new ObjectParameter("NoPaiement", typeof(int));
    
            var noLocationParameter = noLocation != null ?
                new ObjectParameter("NoLocation", noLocation) :
                new ObjectParameter("NoLocation", typeof(string));
    
            var dateParameter = date.HasValue ?
                new ObjectParameter("Date", date) :
                new ObjectParameter("Date", typeof(System.DateTime));
    
            var montantParameter = montant.HasValue ?
                new ObjectParameter("Montant", montant) :
                new ObjectParameter("Montant", typeof(decimal));
    
            var locationPaiementParameter = locationPaiement != null ?
                new ObjectParameter("LocationPaiement", locationPaiement) :
                new ObjectParameter("LocationPaiement", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("InsertPaiementsProcedure", noPaiementParameter, noLocationParameter, dateParameter, montantParameter, locationPaiementParameter);
        }
    
        public virtual int sp_alterdiagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_alterdiagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_creatediagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_creatediagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_dropdiagram(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_dropdiagram", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<sp_helpdiagramdefinition_Result> sp_helpdiagramdefinition(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_helpdiagramdefinition_Result>("sp_helpdiagramdefinition", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<sp_helpdiagrams_Result> sp_helpdiagrams(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_helpdiagrams_Result>("sp_helpdiagrams", diagramnameParameter, owner_idParameter);
        }
    
        public virtual int sp_renamediagram(string diagramname, Nullable<int> owner_id, string new_diagramname)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var new_diagramnameParameter = new_diagramname != null ?
                new ObjectParameter("new_diagramname", new_diagramname) :
                new ObjectParameter("new_diagramname", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_renamediagram", diagramnameParameter, owner_idParameter, new_diagramnameParameter);
        }
    
        public virtual int sp_upgraddiagrams()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_upgraddiagrams");
        }
    }
}
