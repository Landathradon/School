using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace integrateur
{
    /// <summary>
    /// Interaction logic for ActionsAdmin.xaml
    /// </summary>
    public partial class ActionsAdmin : Window
    {
        private BanqueEntities _BanqueEntities;

        List<solde_clients> _infoSoldeClient = new List<solde_clients>();
        Classes.Parametres parametres = new Classes.Parametres();

        bool guichet = false;

        public ActionsAdmin()
        {
            InitializeComponent();
            Refresh();
        }

        private void btn_confirmer_Click(object sender, RoutedEventArgs e)
        {
            if (Txb_montant.Text == string.Empty || Txb_montant.Text == null) { return; }
            decimal montant = decimal.Parse(Txb_montant.Text);
            if (guichet)
            {
                if (montant + parametres.argent_courrant_guichet > parametres.argent_max_guichet)
                {
                    MessageBox.Show($"Vous ne pouvez pas mettre plus de {parametres.argent_max_guichet - parametres.argent_courrant_guichet}$ dans ce guichet.\n", "Trop d'argent !", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else
                {
                    parametres.UpdateParam(Classes.Parametres.Params.argent_courrant_guichet, $"{parametres.argent_courrant_guichet + montant}");
                    MessageBox.Show($"Vous avez ajouter {montant}$ dans le guichet.", "Succès !", MessageBoxButton.OK);
                }

                Close();
            }
            else if (Cmb_compteHypotecaire.SelectedItem is solde_clients compte)
            {
                solde_clients compteMargeCredit = _BanqueEntities.solde_clients.ToList().Find(x => x.idtype_compte == Classes.TypeDeCompte.MargeDeCredit && x.code_client == compte.code_client);

                if (!VerifierSoldeRestant(compte.solde, montant) && compteMargeCredit != null)
                {
                    compteMargeCredit.solde += montant - compte.solde;
                    compte.solde = 0;
                } 
                else if (compteMargeCredit == null)
                {
                    MessageBox.Show("Le solde de ce compte est insuffisant et il n'y a pas de marge de crédit.\nTransaction annulée.", "Solde insuffisant", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else
                {
                    compte.solde -= montant;
                }

                MessageBox.Show($"Vous avez prélever {montant}$ du compte sélectionné.", "Succès !", MessageBoxButton.OK);
                _BanqueEntities.SaveChanges();

                Close();
            }
        }
        private bool VerifierSoldeRestant(decimal solde, decimal montant)
        {
            if (solde < montant) { return false; }
            else { return true; }
        }

        public void ShowAddGuichet()
        {
            Lab_header.Content = "Ajouter de l'argent au guichet";
            Cmb_compteHypotecaire.Visibility = Visibility.Collapsed;
            Lab_balance.Visibility = Visibility.Visible;
            Lab_balance.Content = $"­Il y a {parametres.argent_courrant_guichet}$ dans le guichet";
            guichet = true;
        }

        public void Refresh()
        {
            _BanqueEntities = new BanqueEntities();
            _infoSoldeClient = _BanqueEntities.solde_clients.ToList().FindAll(x => x.idtype_compte == Classes.TypeDeCompte.Hypothecaire);

            Cmb_compteHypotecaire.DataContext = _infoSoldeClient;
        }
    }
}
