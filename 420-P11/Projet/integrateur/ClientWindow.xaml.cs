using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace integrateur
{
    /// <summary>
    /// Interaction logic for ClientWindow.xaml
    /// </summary>
    public partial class ClientWindow : Window
    {
        private BanqueEntities _BanqueEntities;

        client _infoClient = new client();
        List<solde_clients> _infoSoldeClient = new List<solde_clients>();

        public ClientWindow(client _Client)
        {
            InitializeComponent();
            Lab_nomComplet.Content = $"Compte de : { _Client.nom_complet}";
            RafraichirEcran(_Client);
        }

        private void Btn_Depot_Click(object sender, RoutedEventArgs e)
        {
            ActionsClient actionsClient = new ActionsClient(_infoClient, _infoSoldeClient);
            actionsClient.ShowDepot();
            actionsClient.ShowDialog();
            RafraichirEcran(_infoClient);
        }

        private void Btn_Retrait_Click(object sender, RoutedEventArgs e)
        {
            ActionsClient actionsClient = new ActionsClient(_infoClient, _infoSoldeClient);
            actionsClient.ShowRetrait();
            actionsClient.ShowDialog();
            RafraichirEcran(_infoClient);
        }

        private void Btn_Transfert_Click(object sender, RoutedEventArgs e)
        {
            ActionsClient actionsClient = new ActionsClient(_infoClient, _infoSoldeClient);
            actionsClient.ShowTransfert();
            actionsClient.ShowDialog();
            RafraichirEcran(_infoClient);
        }

        private void Btn_Paiement_Click(object sender, RoutedEventArgs e)
        {
            ActionsClient actionsClient = new ActionsClient(_infoClient, _infoSoldeClient);
            actionsClient.ShowPaiement();
            actionsClient.ShowDialog();
            RafraichirEcran(_infoClient);
        }

        private void RafraichirEcran(client _Client)
        {
            _BanqueEntities = new BanqueEntities();
            _infoClient = _Client;
            _infoSoldeClient = _BanqueEntities.solde_clients.ToList().FindAll(x => x.code_client == _Client.code);

            Lbx_comptes.ItemsSource = _infoSoldeClient;

            TxBox_Cheque.Text = _infoSoldeClient.FindAll(x => x.idtype_compte == Classes.TypeDeCompte.Cheque)?.Sum(x => x.solde).ToString("0.00") + "$";
            TxBox_Epargne.Text = _infoSoldeClient.FindAll(x => x.idtype_compte == Classes.TypeDeCompte.Epargne)?.Sum(x => x.solde).ToString("0.00") + "$";
            TxBox_Hypotecaire.Text = _infoSoldeClient.FindAll(x => x.idtype_compte == Classes.TypeDeCompte.Hypothecaire)?.Sum(x => x.solde).ToString("0.00") + "$";
            TxBox_MargeCredit.Text = _infoSoldeClient.FindAll(x => x.idtype_compte == Classes.TypeDeCompte.MargeDeCredit)?.Sum(x => x.solde).ToString("0.00") + "$";

            decimal balance = _infoSoldeClient.FindAll(x => x.idtype_compte != Classes.TypeDeCompte.MargeDeCredit).Sum(x => x.solde);
            balance -= _infoSoldeClient.FindAll(x => x.idtype_compte == Classes.TypeDeCompte.MargeDeCredit).Sum(x => x.solde);
            Lab_balance.Content = $"Balance : {balance.ToString("0.00")}$";
        }
    }
}
