using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace integrateur
{
    /// <summary>
    /// Interaction logic for ActionsClient.xaml
    /// </summary>
    public partial class ActionsClient : Window
    {
        private int TypeTransaction;
        private readonly BanqueEntities _BanqueEntities;

        List<solde_clients> _infoSoldeClient = new List<solde_clients>();
        Classes.Parametres parametres = new Classes.Parametres();

        public ActionsClient(client _Client, List<solde_clients> _SoldeClient)
        {
            InitializeComponent();
            _BanqueEntities = new BanqueEntities();
            _infoSoldeClient = _SoldeClient;

            Cmb_comptePrincipal.DataContext = _infoSoldeClient;
            Cmb_compteSecondaire.DataContext = _infoSoldeClient;
        }

        public void ShowDepot()
        {
            TypeTransaction = Classes.TypeDeTransaction.Depot;
            RafraichirData();

            Lab_Transaction.Content = "Dépot";
            Lab_comptePrincipal.Content = "Dans";
            Grid_compteSecondaire.Visibility = Visibility.Collapsed;
        }

        public void ShowRetrait()
        {
            TypeTransaction = Classes.TypeDeTransaction.Retrait;
            RafraichirData();

            Lab_Transaction.Content = "Retrait";
            Grid_compteSecondaire.Visibility = Visibility.Collapsed;
        }

        public void ShowTransfert()
        {
            TypeTransaction = Classes.TypeDeTransaction.Transfert;
            Cmb_comptePrincipal.DataContext = _infoSoldeClient.FindAll(x => x.idtype_compte == Classes.TypeDeCompte.Cheque);
            Cmb_compteSecondaire.DataContext = _infoSoldeClient.FindAll(x => x.idtype_compte != Classes.TypeDeCompte.Cheque);

            Lab_Transaction.Content = "Transfert";
        }

        public void ShowPaiement()
        {
            TypeTransaction = Classes.TypeDeTransaction.Paiement;
            RafraichirData();

            Lab_Transaction.Content = "Paiement";
            Cmb_compteSecondaire.Visibility = Visibility.Collapsed;

            Lab_compteSecondaire.Content = "#";
            Txb_compteSecondaire.Visibility = Visibility.Visible;
        }

        private void EnregistrerTransaction(object sender, RoutedEventArgs e)
        {
            if (Txb_montant.Text == string.Empty || Txb_montant.Text == null) { return; }
            decimal montantTransaction = decimal.Parse(Txb_montant.Text);
            
            if (TypeTransaction == Classes.TypeDeTransaction.Depot)
            {
                if (Cmb_comptePrincipal.SelectedItem is solde_clients compte)
                {
                    historique_transaction historique_Transaction = new historique_transaction
                    {
                        code_client_envoi = compte.code_client,
                        compte_client_envoi = compte.compte_client,
                        montant = montantTransaction,
                        date = DateTime.Now,
                        idtype_transaction = TypeTransaction
                    };

                    UpdateBanqueEntities(historique_Transaction, $"{montantTransaction}$ à été versé dans votre compte.");
                }
            }
            else if (TypeTransaction == Classes.TypeDeTransaction.Retrait)
            {
                if (Cmb_comptePrincipal.SelectedItem is solde_clients compte)
                {
                    historique_transaction historique_Transaction = new historique_transaction
                    {
                        code_client_envoi = compte.code_client,
                        compte_client_envoi = compte.compte_client,
                        montant = montantTransaction,
                        date = DateTime.Now,
                        idtype_transaction = TypeTransaction
                    };

                    UpdateBanqueEntities(historique_Transaction, $"{montantTransaction}$ à été retirer de votre compte.");
                    parametres.UpdateParam(Classes.Parametres.Params.argent_courrant_guichet, $"{parametres.argent_courrant_guichet - montantTransaction}");
                }

            }
            else if (TypeTransaction == Classes.TypeDeTransaction.Transfert)
            {
                if (Cmb_comptePrincipal.SelectedItem is solde_clients compteEnvoi && Cmb_compteSecondaire.SelectedItem is solde_clients compteRecus)
                {
                    historique_transaction historique_Transaction = new historique_transaction
                    {
                        code_client_envoi = compteEnvoi.code_client,
                        compte_client_envoi = compteEnvoi.compte_client,
                        code_client_recus = compteRecus.code_client,
                        compte_client_recus = compteRecus.compte_client,
                        montant = montantTransaction,
                        date = DateTime.Now,
                        idtype_transaction = TypeTransaction
                    };

                    UpdateBanqueEntities(historique_Transaction, $"{montantTransaction}$ à été transférer dans le compte de votre choix.");
                }
            }
            else if (TypeTransaction == Classes.TypeDeTransaction.Paiement)
            {
                montantTransaction += parametres.frais_transaction;
                if (Cmb_comptePrincipal.SelectedItem is solde_clients compte)
                {
                    historique_transaction historique_Transaction = new historique_transaction
                    {
                        code_client_envoi = compte.code_client,
                        compte_client_envoi = compte.compte_client,
                        montant = montantTransaction,
                        date = DateTime.Now,
                        idtype_transaction = TypeTransaction,
                        no_paiement = Txb_compteSecondaire.Text
                    };

                    UpdateBanqueEntities(historique_Transaction, $"Votre paiement de ${montantTransaction} à été effectué.");
                }
            }
        }

        private List<solde_clients> RafraichirListeSolde(List<solde_clients> _SoldeClient)
        {
            if (TypeTransaction == Classes.TypeDeTransaction.Depot)
            {
                return _SoldeClient.FindAll(x => x.idtype_compte != Classes.TypeDeCompte.MargeDeCredit);
            }
            else if (TypeTransaction == Classes.TypeDeTransaction.Retrait)
            {
                return _SoldeClient.FindAll(x => x.idtype_compte == Classes.TypeDeCompte.Cheque || x.idtype_compte == Classes.TypeDeCompte.Epargne);
            }
            else if (TypeTransaction == Classes.TypeDeTransaction.Paiement)
            {
                return _SoldeClient.FindAll(x => x.idtype_compte == Classes.TypeDeCompte.Cheque);
            }
            else
            {
                return _SoldeClient;
            }
        }

        private void RafraichirData()
        {
            _infoSoldeClient = RafraichirListeSolde(_infoSoldeClient);

            Cmb_comptePrincipal.DataContext = _infoSoldeClient;
            Cmb_compteSecondaire.DataContext = _infoSoldeClient;
        }

        private void VerifierMontant(object sender, RoutedEventArgs e)
        {
            
            if (TypeTransaction != Classes.TypeDeTransaction.Retrait || Txb_montant.Text == string.Empty || Txb_montant.Text == null) { return; }
            decimal montant = decimal.Parse(Txb_montant.Text);
            if (montant > parametres.argent_courrant_guichet)
            {
                MessageBox.Show("Transaction refusée.\nVeuillez contacter votre banque.", "Transaction refusée !", MessageBoxButton.OK, MessageBoxImage.Error);
                Btn_confirmer.IsEnabled = false;
                return;
            }

            if (montant % 10 > 0)
            {
                MessageBox.Show("Vous pouvez seulemment retirer un montant divisible par 10$.", "Attention !", MessageBoxButton.OK, MessageBoxImage.Warning);
                Btn_confirmer.IsEnabled = false;
                return;
            }
            
            if (montant > parametres.retrait_max_guichet)
            {
                MessageBox.Show("Vous ne pouvez pas retirer plus de 1000$", "Attention !", MessageBoxButton.OK, MessageBoxImage.Warning);
                Btn_confirmer.IsEnabled = false;
                return;
            }

            Btn_confirmer.IsEnabled = true;
        }
        private bool VerifierSoldeRestant(decimal solde, decimal montant)
        {
            if(solde < montant) { return false; }
            else { return true; }
        }
        private void UpdateBanqueEntities(historique_transaction historique_Transaction, string message)
        {
            List<solde_clients> _infoSoldes = _BanqueEntities.solde_clients.ToList();

            decimal montantTransaction = historique_Transaction.montant;
            solde_clients compteEnvoi = _infoSoldes.Find(x => x.compte_client == historique_Transaction.compte_client_envoi);
            solde_clients compteMargeCredit = _infoSoldes.Find(x => x.idtype_compte == Classes.TypeDeCompte.MargeDeCredit && x.code_client == historique_Transaction.code_client_envoi);

            if (!VerifierSoldeRestant(compteEnvoi.solde, historique_Transaction.montant) && TypeTransaction == Classes.TypeDeTransaction.Retrait)
            {
                if (MessageBox.Show("Votre solde est insuffisant.\nLe surplus sera mis sur votre marge de crédit.\nVoulez vous continuer?", "Solde insuffisant",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes && compteMargeCredit != null)
                {
                    montantTransaction = compteEnvoi.solde;
                    _infoSoldes.Find(x => x.compte_client == compteMargeCredit.compte_client).solde += historique_Transaction.montant - compteEnvoi.solde;
                }
                else
                {
                    MessageBox.Show("Transaction annulée.", "Annulé", MessageBoxButton.OK);
                    return;
                }
            }
            else if (!VerifierSoldeRestant(compteEnvoi.solde, historique_Transaction.montant) && TypeTransaction != Classes.TypeDeTransaction.Depot)
            {
                MessageBox.Show("Votre solde est insuffisant.\nTransaction annulée.", "Solde insuffisant", MessageBoxButton.OK);
                return;
            }

            try
            {
                _BanqueEntities.historique_transaction.Add(historique_Transaction);

                solde_clients compteARetirer = _infoSoldes.Find(x => x.compte_client == historique_Transaction.compte_client_envoi);
                solde_clients compteACrediter = _infoSoldes.Find(x => x.compte_client == historique_Transaction.compte_client_recus);
                if (TypeTransaction == Classes.TypeDeTransaction.Depot)
                {
                    compteARetirer.solde += historique_Transaction.montant;
                }
                else if(TypeTransaction == Classes.TypeDeTransaction.Retrait || TypeTransaction == Classes.TypeDeTransaction.Paiement)
                {
                    compteARetirer.solde -= montantTransaction;
                }
                else if (TypeTransaction == Classes.TypeDeTransaction.Transfert)
                {
                    compteARetirer.solde -= montantTransaction;
                    
                    if(compteACrediter == compteMargeCredit)
                    { compteACrediter.solde -= historique_Transaction.montant; }
                    else
                    { compteACrediter.solde += historique_Transaction.montant; }
                }

                _BanqueEntities.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "Attention !", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                throw;
            }
            finally
            {
                MessageBox.Show($"{message}", "Succès !", MessageBoxButton.OK);
                Close();
            }
        }
    }
}
