using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace integrateur
{
    /// <summary>
    /// Interaction logic for ModifClientWindow.xaml
    /// </summary>
    public partial class ModifClientWindow : Window
    {
        private enum WindowSelection
        {
            CreeClient,
            CreeCompte,
            ShowHisto,
            ShowAccess
        };

        private readonly BanqueEntities _BanqueEntities;

        List<client> _infoClient = new List<client>();
        List<solde_clients> _infoSoldeClient = new List<solde_clients>();

        private bool _ClientAccepter = true;

        WindowSelection WindowChoice = WindowSelection.CreeClient;

        public ModifClientWindow()
        {
            InitializeComponent();
            _BanqueEntities = new BanqueEntities();
            _infoClient = _BanqueEntities.clients.ToList();
            _infoSoldeClient = _BanqueEntities.solde_clients.ToList();
            Cmb_UserList.DataContext = _infoClient;
        }

        public void ShowClient()
        {
            WindowChoice = WindowSelection.CreeClient;

            client client = new client();
            client.compte_actif = 1;

            _infoClient.Add(client);
            Cmb_UserList.SelectedItem = client;

            Lab_ModifClient.Content = "Crée client";
            Grid_compte.IsEnabled = false;
            Grid_Histo.IsEnabled = false;
            Chk_actif.IsEnabled = false;
        }

        public void ShowCompte()
        {
            WindowChoice = WindowSelection.CreeCompte;

            Lab_ModifClient.Content = "Ajouter comptes";
            Btn_Cree.IsEnabled = false;
            Btn_Modif.IsEnabled = false;
            Grid_client.IsEnabled = false;
            Grid_Histo.IsEnabled = false;
            Chk_actif.IsEnabled = false;
        }

        public void ShowHisto()
        {
            WindowChoice = WindowSelection.ShowHisto;

            Lab_ModifClient.Content = "Afficher transactions";
            Grid_client.IsEnabled = false;
            Grid_compte.IsEnabled = false;
            Chk_actif.IsEnabled = false;
        }
        public void ShowAcces()
        {
            WindowChoice = WindowSelection.ShowAccess;

            Lab_ModifClient.Content = "Modifier l'accès";
            Grid_client.IsEnabled = false;
            Grid_compte.IsEnabled = false;
            Grid_Histo.IsEnabled = false;
        }

        private void Cmb_UserList_OnChange(object sender, SelectionChangedEventArgs e)
        {

            if (Cmb_UserList.SelectedItem is client sClient)
            {
                if (sClient.code != null && WindowChoice == WindowSelection.CreeClient)
                {
                    Btn_Cree.IsEnabled = false;
                    Btn_Modif.IsEnabled = true;
                }
                else if(WindowChoice == WindowSelection.CreeClient)
                {
                    Btn_Cree.IsEnabled = true;
                    Btn_Modif.IsEnabled = false;
                }

                TxBox_Code.Text =       sClient.code;
                TxBox_Prenom.Text =     sClient.prenom;
                TxBox_Nom.Text =        sClient.nom;
                TxBox_Telephone.Text =  sClient.telephone;
                TxBox_Courriel.Text =   sClient.courriel;
                TxBox_NIP.Text =        sClient.nip.ToString();
                Chk_actif.IsChecked =   sClient.compte_actif == 1 ? true: false;
            }

            if (WindowChoice != WindowSelection.CreeClient)
            {
                UpdateHistoriqueTransactions(Classes.TypeDeCompte.Cheque);
            }
            
        }

        private void Btn_Cree_OnClick(object sender, RoutedEventArgs e)
        {

            if (TxBox_Code.Text == string.Empty || TxBox_Prenom.Text == string.Empty || TxBox_Nom.Text == string.Empty ||
                TxBox_Telephone.Text == string.Empty || TxBox_Courriel.Text == string.Empty || TxBox_NIP.Text == string.Empty)
            {
                _ClientAccepter = false;
                MessageBox.Show("Veuillez remplir les informations correctement.", "Attention !", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                try
                {
                    if (Cmb_UserList.SelectedItem is client sClient)
                    {
                        sClient.code = TxBox_Code.Text;
                        sClient.prenom = TxBox_Prenom.Text;
                        sClient.nom = TxBox_Nom.Text;
                        sClient.telephone = TxBox_Telephone.Text;
                        sClient.courriel = TxBox_Courriel.Text;
                        sClient.nip = int.Parse(TxBox_NIP.Text);
                        sClient.compte_actif = (short)((bool)Chk_actif.IsChecked ? 1 : 0);

                        _BanqueEntities.clients.Add(sClient);
                    }

                    _BanqueEntities.SaveChanges();
                }
                catch (Exception ex)
                {
                    _ClientAccepter = false;
                    MessageBox.Show("" + ex, "Attention !", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    throw;
                }
                finally
                {
                    if (_ClientAccepter)
                    {
                        MessageBox.Show("Ce nouveau client à été ajouté.", "Succès !", MessageBoxButton.OK);
                    }

                    Close();
                }
            }
        }

        private void Btn_Modif_OnClick(object sender, RoutedEventArgs e)
        {
            if (TxBox_Code.Text == string.Empty || TxBox_Prenom.Text == string.Empty || TxBox_Nom.Text == string.Empty ||
                    TxBox_Telephone.Text == string.Empty || TxBox_Courriel.Text == string.Empty || TxBox_NIP.Text == string.Empty)
            {
                _ClientAccepter = false;
                MessageBox.Show("Veuillez remplir les informations correctement.", "Attention !", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                try
                {
                    if (Cmb_UserList.SelectedItem is client sClient)
                    {
                        sClient.code = TxBox_Code.Text;
                        sClient.prenom = TxBox_Prenom.Text;
                        sClient.nom = TxBox_Nom.Text;
                        sClient.telephone = TxBox_Telephone.Text;
                        sClient.courriel = TxBox_Courriel.Text;
                        sClient.nip = int.Parse(TxBox_NIP.Text);
                        sClient.compte_actif = (short)((bool)Chk_actif.IsChecked ? 1 : 0);
                    }

                    _BanqueEntities.SaveChanges();
                }
                catch (Exception ex)
                {
                    _ClientAccepter = false;
                    MessageBox.Show("" + ex, "Attention !", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    throw;
                }
                finally
                {
                    if (_ClientAccepter)
                    {
                        MessageBox.Show("Ce client à bien été modifié.", "Succès !", MessageBoxButton.OK);
                    }
                }
            }
        }

        private void AddCompteClient(client sClient, int typeDeCompte)
        {
            try
            {
                solde_clients solde_Client = new solde_clients();

                solde_Client.compte_client = sClient.code + "_" + GetLastCompteNumber();
                solde_Client.code_client = sClient.code;
                solde_Client.idtype_compte = typeDeCompte;
                solde_Client.solde = 0;

                _BanqueEntities.solde_clients.Add(solde_Client);

                _BanqueEntities.SaveChanges();
            }
            catch (Exception ex)
            {
                _ClientAccepter = false;
                MessageBox.Show("" + ex, "Attention !", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                throw;
            }
            finally
            {
                if (_ClientAccepter)
                {
                    MessageBox.Show($"Le compte de type: {Classes.TypeDeCompte.ReturnName(typeDeCompte)} à été ajouté.", "Succès !", MessageBoxButton.OK);
                    _infoSoldeClient = _BanqueEntities.solde_clients.ToList();
                }
            }
        }

        private int GetLastCompteNumber()
        {
            return _infoSoldeClient.Count + 1;
        }

        private void AjoutCompte_OnClick(object sender, RoutedEventArgs e)
        {
            Button Btn_clicked = e.Source as Button;
            
            bool CompteChequeExiste;
            bool CompteMargeCreditExiste;

            if (Cmb_UserList.SelectedItem is client sClient)
            {
                CompteChequeExiste = _infoSoldeClient.Find(x => x.code_client == sClient.code && x.idtype_compte == Classes.TypeDeCompte.Cheque) != null;
                CompteMargeCreditExiste = _infoSoldeClient.Find(x => x.code_client == sClient.code && x.idtype_compte == Classes.TypeDeCompte.MargeDeCredit) != null;

                if (Btn_clicked.Name == Btn_compteCheque.Name)
                {
                    AddCompteClient(sClient, Classes.TypeDeCompte.Cheque);
                }
                else if (Btn_clicked.Name == Btn_compteEpargne.Name && CompteChequeExiste)
                {
                    AddCompteClient(sClient, Classes.TypeDeCompte.Epargne);
                }
                else if (Btn_clicked.Name == Btn_compteHypothecaire.Name && CompteChequeExiste)
                {
                    AddCompteClient(sClient, Classes.TypeDeCompte.Hypothecaire);
                }
                else if (Btn_clicked.Name == Btn_compteMargeCredit.Name && CompteChequeExiste && !CompteMargeCreditExiste)
                {
                    AddCompteClient(sClient, Classes.TypeDeCompte.MargeDeCredit);
                }
                else if (Btn_clicked.Name == Btn_compteMargeCredit.Name && CompteMargeCreditExiste)
                {
                    MessageBox.Show("Vous ne pouvez pas ajouter plus d'une marge de crédit.", "Attention !", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                else if (!CompteChequeExiste)
                {
                    MessageBox.Show("Veuillez ajouter un chèque avant d'ajouter les autres.", "Attention !", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }

            }
            else { return; }

        }

        private void UpdateHistoriqueTransactions(int typeDeCompte)
        {
            if (Cmb_UserList.SelectedItem is client sClient)
            {
                List<historique_transaction> _listePourAfficher = new List<historique_transaction>();

                if (typeDeCompte == Classes.TypeDeCompte.MargeDeCredit)
                {
                    _listePourAfficher = _BanqueEntities.historique_transaction.ToList().FindAll(x => x.idtype_transaction == Classes.TypeDeTransaction.Transfert 
                        && x.typeCompteRecus == typeDeCompte && (x.code_client_envoi == sClient.code || x.code_client_recus == sClient.code));
                }
                else
                {
                    _listePourAfficher = _BanqueEntities.historique_transaction.ToList().FindAll(x =>
                        x.typeCompteEnvoi == typeDeCompte && (x.code_client_envoi == sClient.code || x.code_client_recus == sClient.code));
                }
                
                Lv_historique.ItemsSource = _listePourAfficher;
            }
        }

        private void mnu_cheque_Click(object sender, RoutedEventArgs e)
        {
            UpdateHistoriqueTransactions(Classes.TypeDeCompte.Cheque);
        }

        private void mnu_epargne_Click(object sender, RoutedEventArgs e)
        {
            UpdateHistoriqueTransactions(Classes.TypeDeCompte.Epargne);
        }

        private void mnu_hypothecaire_Click(object sender, RoutedEventArgs e)
        {
            UpdateHistoriqueTransactions(Classes.TypeDeCompte.Hypothecaire);
        }

        private void mnu_margeCredit_Click(object sender, RoutedEventArgs e)
        {
            UpdateHistoriqueTransactions(Classes.TypeDeCompte.MargeDeCredit);
        }
    }
}
