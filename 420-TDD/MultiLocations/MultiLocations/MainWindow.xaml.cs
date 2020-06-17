using System.Windows;

namespace MultiLocations
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void mnuAjouterLoc_OnClick(object sender, RoutedEventArgs e)
        {
            AjoutLocation ajoutLocation = new AjoutLocation();

            ajoutLocation.ShowDialog();
        }

        private void mnuModiferLoc_OnClick(object sender, RoutedEventArgs e)
        {
            ModifLocation modifLocation = new ModifLocation();

            modifLocation.ShowDialog();
        }

        private void mnuAjouterPay_OnClick(object sender, RoutedEventArgs e)
        {
            AjoutPaiement ajoutPaiement = new AjoutPaiement();

            ajoutPaiement.ShowDialog();
        }

        private void mnuAnnulerPay_OnClick(object sender, RoutedEventArgs e)
        {
            AnnulerPaiement annulerPaiement = new AnnulerPaiement();

            annulerPaiement.ShowDialog();
        }

        private void mnuQuit_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}