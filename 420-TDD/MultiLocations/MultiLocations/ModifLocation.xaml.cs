using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MultiLocations
{
    public partial class ModifLocation : Window
    {
        private readonly LoanEntities _loanEntities;
        private readonly AjoutLocation _ajoutLocation;

        private bool _locationAccepter = true;

        // private bool _paiementAccepter = false;
        List<Location> _infoLocations = new List<Location>();
        List<int> _infoTerme = new List<int> {1, 2, 3, 4};
        List<Vehicle> _infoVehicle = new List<Vehicle>();
        List<Client> _infoClient = new List<Client>();
        List<int> _infoNbrPaiement = new List<int> {12, 24, 36, 48};

        public ModifLocation()
        {
            InitializeComponent();
            _loanEntities = new LoanEntities();
            _infoLocations = _loanEntities.Locations.ToList();
            _infoVehicle = _loanEntities.Vehicles.ToList();
            _infoClient = _loanEntities.Clients.ToList();
            IDLocationList.DataContext = _infoLocations;
            TermeList.DataContext = _infoTerme;
            NivList.DataContext = _infoVehicle;
            ClientList.DataContext = _infoClient;
            NbrPaiementList.DataContext = _infoNbrPaiement;
        }

        private void IDLocationList_OnChange(object sender, SelectionChangedEventArgs e)
        {
            if (IDLocationList.SelectedItem is Location sLocation)
            {
                DateDebutDP.SelectedDate = sLocation.DateLocation;
                PremierPaiementDP.SelectedDate = sLocation.DatePaiement;
                MontantTxt.Text = sLocation.MontantPaiement.ToString();
                TermeList.Text = sLocation.TermesDeLocation.NbrAnnees.ToString();
                NivList.Text = sLocation.VINVehicule;
                ClientList.Text = sLocation.Client.NomComplet;
                NbrPaiementList.SelectedValue = sLocation.NbrPaiement;
            }
        }

        private void BtnModifier_OnClick(object sender, RoutedEventArgs e)
        {
            if (MontantTxt.Text == String.Empty || DateDebutDP.SelectedDate == null ||
                PremierPaiementDP.SelectedDate == null)
            {
                _locationAccepter = false;
                MessageBox.Show("Veuillez remplir les informations correctement", "Attention !", MessageBoxButton.OK,
                    MessageBoxImage.Exclamation);
                Close();
            }
            else
            {
                if (MessageBox.Show("Voulez vous vraiment modifier cette location?", "Modification de location",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        if (IDLocationList.SelectedItem is Location sLocation)
                        {
                            sLocation.DateLocation = DateDebutDP.SelectedDate;
                            sLocation.DatePaiement = PremierPaiementDP.SelectedDate;
                            sLocation.MontantPaiement = decimal.Parse(MontantTxt.Text);
                            sLocation.NbrPaiement = int.Parse(NbrPaiementList.SelectedValue.ToString());
                            sLocation.VINVehicule = _infoVehicle[NivList.SelectedIndex].VINVehicule;
                            sLocation.NoClient = _infoClient[ClientList.SelectedIndex].NoClient;
                            sLocation.TermesDeLocation.NbrAnnees = byte.Parse(TermeList.SelectedValue.ToString());
                            sLocation.TermesDeLocation.KiloMax = int.Parse(TermeList.SelectedValue.ToString()) * 20000;
                            sLocation.TermesDeLocation.Prime = decimal.Parse(TermeList.SelectedValue.ToString()) * 150;
                        }

                        _loanEntities.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        _locationAccepter = false;
                        MessageBox.Show("" + ex, "Attention !", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        throw;
                    }
                    finally
                    {
                        if (_locationAccepter)
                        {
                            MessageBox.Show("Votre nouvelle location à été enregistrer.", "Succès !",
                                MessageBoxButton.OK);
                        }

                        Close();
                    }
                }
                else
                {
                    Close();
                }
            }
        }
    }
}