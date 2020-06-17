using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace LINQ_Exercice_3
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int[] nombres = new int[] { 55, 75, 80, 66, 45, 68, 95, 82, 77, 62 };

        public MainWindow()
        {
            InitializeComponent();
        }

        private void afficherListe_Click(object sender, RoutedEventArgs e)
        {
            string resultat = string.Empty;

            foreach (int nombre in nombres)
            {
                resultat += nombre.ToString() + ", ";
            }
            int index = resultat.LastIndexOf(",", StringComparison.Ordinal);
            resultat = resultat.Remove(index);
            Liste.Text = resultat;
        }

        private void afficherPetit_Click(object sender, RoutedEventArgs e)
        {
            int resultat = nombres.Min();

            petitNombre.Text = resultat.ToString();
        }

        private void afficherGrand_Click(object sender, RoutedEventArgs e)
        {
            int resultat = nombres.Max();

            grandNombre.Text = resultat.ToString();
        }

        private void afficherSomme_Click(object sender, RoutedEventArgs e)
        {
            int resultat = nombres.Sum();

            sommeNombre.Text = resultat.ToString();
        }

        private void afficherQuantite_Click(object sender, RoutedEventArgs e)
        {
            int compteur = nombres.Length;

            quantiteNombre.Text = compteur.ToString();
        }

        private void afficherMoyenne_Click(object sender, RoutedEventArgs e)
        {
            double moyenne = nombres.Average();
            moyenneNombre.Text = moyenne.ToString();
        }
    }
}
