using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace MultiLocations
{
    public partial class AjoutPaiement : Window
    {
        private LoanEntities _loanEntities;
        private InfoPaiement _paiementData;
        private bool _paiementAccepter = true;
        List<Location> _locationsList = new List<Location>();
        List<InfoPaiement> _infoPaiements = new List<InfoPaiement>();
        public AjoutPaiement()
        {
            InitializeComponent();
            _loanEntities = new LoanEntities();
            _locationsList = _loanEntities.Locations.ToList();
            IDLocationList.DataContext = _locationsList;
        }

        private void BtnPayer_OnClick(object sender, RoutedEventArgs e)
        {
            _loanEntities = new LoanEntities();
            
            _infoPaiements = _loanEntities.InfoPaiements.ToList();
            int dernierPaiementId = _infoPaiements[_infoPaiements.Count-1].NoPaiement + 1; //Select last index and add + 1
            if (MontantTxt.Text != string.Empty || DatePaiementDP.SelectedDate != null) // Vérifier si les informations sont remplies
            {
                _paiementData = new InfoPaiement
                {
                    NoPaiement = dernierPaiementId,
                    NoLocation = _locationsList[IDLocationList.SelectedIndex].NoLocation,
                    Date = DatePaiementDP.SelectedDate,
                    Montant = decimal.Parse(MontantTxt.Text),
                    LocationPaiement = "App"
                };
            }
            else
            {
                _paiementAccepter = false;
                MessageBox.Show("Veuillez remplir les informations correctement", "Attention !", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                Close();
            }

            try
            {
                _loanEntities.InsertPaiementsProcedure(_paiementData.NoPaiement, _paiementData.NoLocation,
                    _paiementData.Date, _paiementData.Montant, _paiementData.LocationPaiement);
            }
            catch (Exception ex)
            {
                _paiementAccepter = false;
                MessageBox.Show(ex.Message, "Attention !", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                throw;
            }
            finally
            {
                if (_paiementAccepter)
                {
                    MessageBox.Show("Votre paiement à été enregistrer.", "Succès !", MessageBoxButton.OK);
                }
                Close();
            }
        }
    }
}