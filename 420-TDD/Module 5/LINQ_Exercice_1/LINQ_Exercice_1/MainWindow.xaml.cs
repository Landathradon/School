using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace LINQ_Exercice_1
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int[] nombres = {1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20};

        public MainWindow()
        {
            InitializeComponent();
        }

        private void AfficherListe_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<int> selection = from nombre in nombres select nombre;
            
            
            foreach (int nombre in selection)
            {
                Complete.Items.Add(nombre);
            }
        }

        private void AfficherPair_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<int> selPair = from nombre in nombres
                where (nombre % 2 == 0)
                select nombre;
            
            foreach (int nombre in selPair)
            {
                Pair.Items.Add(nombre);
            }
        }

        private void AfficherImpair_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<int> selImpair = from nombre in nombres
                where (nombre % 2 != 0)
                select nombre;
            
            foreach (int nombre in selImpair)
            {
                Impair.Items.Add(nombre);
            }
        }
    }
}
