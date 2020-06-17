using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace MultiLocations
{
    public partial class AjoutLocation : Window
    {
        private LoanEntities _loanEntities;
        private bool LocationAccepter = true;
        // private bool _paiementAccepter = false;
        List<int> _infoTerme = new List<int> { 1, 2, 3, 4 };
        List<Vehicle> _infoVehicle = new List<Vehicle>();
        List<Client> _infoClient = new List<Client>();
        List<int> _infoNbrPaiement = new List<int> { 12, 24, 36, 48 };
        public AjoutLocation()
        {
            InitializeComponent();
            _loanEntities = new LoanEntities();
            _infoVehicle = _loanEntities.Vehicles.ToList();
            _infoClient = _loanEntities.Clients.ToList();
            TermeList.DataContext = _infoTerme;
            NivList.DataContext = _infoVehicle;
            ClientList.DataContext = _infoClient;
            NbrPaiementList.DataContext = _infoNbrPaiement;
        }

        private void BtnAjouter_OnClick(object sender, RoutedEventArgs e)
        {
            if (MontantTxt.Text == String.Empty || DateDebutDP.SelectedDate == null ||
                PremierPaiementDP.SelectedDate == null)
            {
                LocationAccepter = false;
                MessageBox.Show("Veuillez remplir les informations correctement", "Attention !", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                Close();
            }
            else
            {
                int noLocation = int.Parse(RemoveSpaces(_loanEntities.Locations.ToList()[_loanEntities.Locations.Count() - 1].NoLocation)) + 1;
                DateTime? dateLocation = DateDebutDP.SelectedDate;
                DateTime? datePaiement = PremierPaiementDP.SelectedDate;
                decimal montant = decimal.Parse(MontantTxt.Text);
                int nbrPaiement = int.Parse(NbrPaiementList.SelectedValue.ToString());
                string vINVehicle = _infoVehicle[NivList.SelectedIndex].VINVehicule;
                string noClient = _infoClient[ClientList.SelectedIndex].NoClient;
                byte nbrAnnee = byte.Parse(TermeList.SelectedValue.ToString());
                int kiloMax = int.Parse(TermeList.SelectedValue.ToString()) * 20000;
                decimal prime = decimal.Parse(TermeList.SelectedValue.ToString()) * 150;
                
                try
                {
                    _loanEntities.CreateLeaseProcedure(noLocation.ToString(), dateLocation, datePaiement, montant, 
                        nbrPaiement,vINVehicle, noClient,nbrAnnee, kiloMax, prime);
                }
                catch (Exception ex)
                {
                    LocationAccepter = false;
                    MessageBox.Show("" + ex, "Attention !", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    throw;
                }
                finally
                {
                    if (LocationAccepter)
                    {
                        MessageBox.Show("Votre nouvelle location à été enregistrer.", "Succès !", MessageBoxButton.OK);
                    }
                    Close();
                }
            }
        }
        
        public string RemoveSpaces(string str)  
        { 
            str = str.Replace(" ","");
            return str; 
        } 
    }
}