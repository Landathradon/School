using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace LINQ_Exercice_2
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Etudiant> etudiant = new List<Etudiant>();
        
        public MainWindow()
        {
            InitializeComponent();

            etudiant = Etudiant.RecupererEtudiants();
        }

        private void btnMasculin_Click(object sender, RoutedEventArgs e)
        {
            lstEtudiant.Items.Clear();

            IEnumerable<Etudiant> selMasculin = 
                Enumerable.Where(etudiant, etu => !etu.Sexe).OrderBy(etu => etu.Nom);

            foreach (Etudiant etudiant in selMasculin)
            {
                lstEtudiant.Items.Add(etudiant.Nom);
            }
        }

        private void btnFeminin_Click(object sender, RoutedEventArgs e)
        {
            lstEtudiant.Items.Clear();

            IEnumerable<Etudiant> selFeminin =
                Enumerable.Where(etudiant, etu => etu.Sexe).OrderByDescending(etu => etu.Nom);

            foreach (Etudiant etudiant in selFeminin)
            {
                lstEtudiant.Items.Add(etudiant.Nom);
            }

        }
    }
}
