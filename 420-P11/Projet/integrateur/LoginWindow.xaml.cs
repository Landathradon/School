using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace integrateur
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LoginWindow
    {
        private readonly BanqueEntities _BanqueEntities;

        List<client> _infoClient = new List<client>();
        List<parametre> _infoParametres = new List<parametre>();
        Classes.Parametres parametres = new Classes.Parametres();

        int TentativesConnexion = 0;

        public LoginWindow()
        {
            InitializeComponent();
            _BanqueEntities = new BanqueEntities();
            _infoClient = _BanqueEntities.clients.ToList();
            _infoParametres = _BanqueEntities.parametres.ToList();
        }

        private void Btn_Valider_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            string Code = TxBox_Code.Text;
            string NIP = TxBox_NIP.Password;

            if ((bool)Chk_Admin.IsChecked)
            {
                string compte_admin_code = _infoParametres.Find(x => x.description == "compte_admin_code").valeur;
                string compte_admin_pass = _infoParametres.Find(x => x.description == "compte_admin_pass").valeur;

                if (Code == compte_admin_code && NIP == compte_admin_pass)
                {
                    AdminWindow adminWindow = new AdminWindow();
                    adminWindow.Show();
                    Close();
                }
            } 
            else
            {
                client client = _infoClient.Find(x => x.code == Code);

                if (!parametres.guichet_ouvert)
                {
                    MessageBox.Show("Ce guichet est fermé.\nVeuillez en utiliser un autre.", "Attention !", MessageBoxButton.OK, MessageBoxImage.Warning);
                    Close();
                }

                if(client == null)
                {
                    MessageBox.Show("Ce client n'existe pas. Veuillez réessayer.", "Attention !", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                if (client.compte_actif == 0)
                {
                    MessageBox.Show("Votre compte est vérouiller.\nVeuillez contacter votre banque.", "Attention !", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                if (client.nip.ToString() == NIP && client.compte_actif == 1)
                {
                    ClientWindow clientWindow = new ClientWindow(client);
                    clientWindow.Show();
                    Close();
                } 
                else if (client.nip.ToString() != NIP && client.compte_actif == 1)
                {
                    MessageBox.Show("Vous avez entrer le mauvais NIP\nVeuillez réessayer.", "Attention !", MessageBoxButton.OK, MessageBoxImage.Warning);

                    TentativesConnexion++;

                    if (TentativesConnexion >= 3)
                    {
                        client.compte_actif = 0;

                        _BanqueEntities.SaveChanges();

                        MessageBox.Show("Votre compte à été vérouiller.\nVeuillez contacter un administrateur.", "Attention !", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }

            TxBox_Code.Text = string.Empty;
            TxBox_NIP.Password = string.Empty;

        }

        private void ShowButtons(object sender, RoutedEventArgs e)
        {
            if(Grid_nip.Visibility == Visibility.Collapsed)
            {
                Grid_nip.Visibility = Visibility.Visible;
            }
            else
            {
                Grid_nip.Visibility = Visibility.Collapsed;
            }
        }

        private void Btn_nip_0_Click(object sender, RoutedEventArgs e)
        {
            TxBox_NIP.Password += 0;
        }

        private void Btn_nip_1_Click(object sender, RoutedEventArgs e)
        {
            TxBox_NIP.Password += 1;
        }

        private void Btn_nip_2_Click(object sender, RoutedEventArgs e)
        {
            TxBox_NIP.Password += 2;
        }

        private void Btn_nip_3_Click(object sender, RoutedEventArgs e)
        {
            TxBox_NIP.Password += 3;
        }

        private void Btn_nip_4_Click(object sender, RoutedEventArgs e)
        {
            TxBox_NIP.Password += 4;
        }

        private void Btn_nip_5_Click(object sender, RoutedEventArgs e)
        {
            TxBox_NIP.Password += 5;
        }

        private void Btn_nip_6_Click(object sender, RoutedEventArgs e)
        {
            TxBox_NIP.Password += 6;
        }

        private void Btn_nip_7_Click(object sender, RoutedEventArgs e)
        {
            TxBox_NIP.Password += 7;
        }

        private void Btn_nip_8_Click(object sender, RoutedEventArgs e)
        {
            TxBox_NIP.Password += 8;
        }

        private void Btn_nip_9_Click(object sender, RoutedEventArgs e)
        {
            TxBox_NIP.Password += 9;
        }
    }
}