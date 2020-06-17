using System.Collections.Generic;

namespace LINQ_Exercice_2
{
    class Etudiant
    {
        public int ID { get; set; }
        public string Nom { get; set; }
        // La propriété Sexe prend la valeur false pour Masculin et true pour Féminin.
        public bool Sexe { get; set; }

        public static List<Etudiant> RecupererEtudiants()
        {
            List<Etudiant> listeEtudiants = new List<Etudiant>();

            listeEtudiants.Add(new Etudiant { ID = 101, Nom = "Serge", Sexe = false });
            listeEtudiants.Add(new Etudiant { ID = 102, Nom = "Valérie", Sexe = true });
            listeEtudiants.Add(new Etudiant { ID = 103, Nom = "Alexandre", Sexe = false });
            listeEtudiants.Add(new Etudiant { ID = 104, Nom = "Mélanie", Sexe = true });
            listeEtudiants.Add(new Etudiant { ID = 105, Nom = "Alexandra", Sexe = true });
            listeEtudiants.Add(new Etudiant { ID = 106, Nom = "Yves", Sexe = false });
            listeEtudiants.Add(new Etudiant { ID = 107, Nom = "François", Sexe = false });
            listeEtudiants.Add(new Etudiant { ID = 108, Nom = "Lounis", Sexe = false });
            listeEtudiants.Add(new Etudiant { ID = 109, Nom = "Danielle", Sexe = true });
            listeEtudiants.Add(new Etudiant { ID = 110, Nom = "Martin", Sexe = false });

            return listeEtudiants;
        }
    }
}
