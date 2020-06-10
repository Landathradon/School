using System.Security.Principal;

namespace SuiviEtudiants
{
    public class Etudiant
    {
        public string IdEtudiant { get; set; }
        public string Prenom { get; set; }
        public string Nom { get; set; }

        public string NomComplet => Nom.ToUpper() + ", " + Prenom;
    }
}