using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MultiLocations
{
    public partial class AnnulerPaiement : Window
    {
        private LoanEntities _loanEntities;
        private bool _paiementAccepter = false;
        List<InfoPaiement> _infoPaiements = new List<InfoPaiement>();
        public AnnulerPaiement()
        {
            InitializeComponent();
            _loanEntities = new LoanEntities();
            _infoPaiements = _loanEntities.InfoPaiements.ToList();
            IDPaiementList.DataContext = _infoPaiements;
        }

        private void IDPaiementList_OnChange(object sender, SelectionChangedEventArgs e)
        {
            IDLocationTxt.Text = _infoPaiements[IDPaiementList.SelectedIndex].NoLocation;
            MontantTxt.Text = _infoPaiements[IDPaiementList.SelectedIndex].Montant.ToString("F") + " $";
        }

        private void BtnAnnuler_OnClick(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Voulez vous vraiment annuler ce paiement?", "Annulation de paiement", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                //Load current used data to use
                int noPaiement = _infoPaiements[IDPaiementList.SelectedIndex].NoPaiement;

                try
                {
                    _loanEntities.InfoPaiements.Remove(_loanEntities.InfoPaiements.Single(a => a.NoPaiement == noPaiement));
                    _loanEntities.SaveChanges();
                    _paiementAccepter = true;
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, "Attention !", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    throw;
                }
                finally
                {
                    if (_paiementAccepter)
                    {
                        MessageBox.Show("Votre paiement à correctement été annuler", "Confirmation");
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