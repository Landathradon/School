using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace integrateur
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        private BanqueEntities _BanqueEntities;

        List<solde_clients> _infoSoldeClient = new List<solde_clients>();
        Classes.Parametres parametres = new Classes.Parametres();
        public AdminWindow()
        {
            InitializeComponent();
            Refresh();
        }

        private void Btn_CreeClient_Click(object sender, RoutedEventArgs e)
        {
            ModifClientWindow modifClientWindow = new ModifClientWindow();
            modifClientWindow.ShowClient();
            modifClientWindow.ShowDialog();
        }

        private void Btn_CreeCompte_Click(object sender, RoutedEventArgs e)
        {
            ModifClientWindow modifClientWindow = new ModifClientWindow();
            modifClientWindow.ShowCompte();
            modifClientWindow.ShowDialog();
        }

        private void Btn_Transaction_Click(object sender, RoutedEventArgs e)
        {
            ModifClientWindow modifClientWindow = new ModifClientWindow();
            modifClientWindow.ShowHisto();
            modifClientWindow.ShowDialog();
        }

        private void Btn_GererAcces_Click(object sender, RoutedEventArgs e)
        {
            ModifClientWindow modifClientWindow = new ModifClientWindow();
            modifClientWindow.ShowAcces();
            modifClientWindow.ShowDialog();
        }

        private void Btn_fermerGuichet_Click(object sender, RoutedEventArgs e)
        {

            bool ouvert = parametres.guichet_ouvert;
            string message = ouvert ? "fermer" : "ouvert";
            parametres.UpdateParam(Classes.Parametres.Params.guichet_ouvert, ouvert ? "0": "1");
            MessageBox.Show($"Vous avez {message} le guichet.", "Succès !", MessageBoxButton.OK);
            Refresh();
        }

        public void UpdateButtons(bool ouvert)
        {
            Btn_fermerGuichet.Content = ouvert ? "Fermer le guichet" : "Ouvrir le guichet";
        }

        private void Btn_interet_Click(object sender, RoutedEventArgs e)
        {
            decimal interet = 1.01M;
            _infoSoldeClient.FindAll(x => x.idtype_compte == Classes.TypeDeCompte.Epargne).ForEach(x => x.solde *= interet);
            MessageBox.Show($"Vous avez augmenter de 1% le solde des comptes épargnes.", "Succès !", MessageBoxButton.OK);
            _BanqueEntities.SaveChanges();
        }

        private void Btn_prendreMontant_Click(object sender, RoutedEventArgs e)
        {
            ActionsAdmin actionsAdmin = new ActionsAdmin();
            actionsAdmin.ShowDialog();
        }

        private void Btn_augmSoldeMarge_Click(object sender, RoutedEventArgs e)
        {
            decimal interet = 1.05M;
            _infoSoldeClient.FindAll(x => x.idtype_compte == Classes.TypeDeCompte.MargeDeCredit).ForEach(x => x.solde *= interet);
            MessageBox.Show($"Vous avez augmenter de 5% le solde des marges de crédit.", "Succès !", MessageBoxButton.OK);
            _BanqueEntities.SaveChanges();
        }

        private void Btn_ajoutArgentGuichet_Click(object sender, RoutedEventArgs e)
        {
            ActionsAdmin actionsAdmin = new ActionsAdmin();
            actionsAdmin.ShowAddGuichet();
            actionsAdmin.ShowDialog();
            Refresh();
        }
        public void Refresh()
        {
            _BanqueEntities = new BanqueEntities();
            _infoSoldeClient = _BanqueEntities.solde_clients.ToList();
            parametres = new Classes.Parametres();
            UpdateButtons(parametres.guichet_ouvert);
        }
    }
}
