using System;
using System.ComponentModel;
using System.Windows;
using System.Configuration;
using System.Data.SqlClient;

namespace SuiviEtudiants
{
    public partial class frmLoggin : Window
    {
        private SqlConnection connexion;
        private SqlCommand commande;
        private bool ouverture = false;

        public frmLoggin()
        {
            InitializeComponent();
            connexion = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
            txtUtilisateur.Focus();
            ouverture = true;
        }
        
        private void BtnOk_OnClick(object sender, RoutedEventArgs e)
        {
            string user = txtUtilisateur.Text;
            string passwd = txtMotPasse.Password;

            try
            {
                string authentification = "SELECT * FROM tblUtilisateurs WHERE NomUtilisateur = '" +
                                          txtUtilisateur.Text + "' AND MotPasse = '" + txtMotPasse.Password + "'";
                commande = new SqlCommand(authentification, connexion);
                connexion.Open();
                SqlDataReader lecteur = commande.ExecuteReader();

                if (lecteur.Read())
                {
                    UtilisateurActif utilisateur = new UtilisateurActif();

                    utilisateur.IdUtilisateur = lecteur["IdUtilisateur"].ToString();
                    utilisateur.Prenom = lecteur["Prenom"].ToString();
                    utilisateur.Nom = lecteur["Nom"].ToString();

                    MessageBox.Show("Bienvenue " + utilisateur.Prenom + " " + utilisateur.Nom);
                    
                    SuiviEtudiantsUI gestionEtudiant = new SuiviEtudiantsUI(utilisateur);

                    gestionEtudiant.Show();
                    ouverture = false;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Les informations saisies ne sont pas valides.");

                    txtUtilisateur.Text = string.Empty; //Empty values
                    txtMotPasse.Password = string.Empty; // ^

                    txtUtilisateur.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connexion.Close();
            }
        }

        private void BtnAnnuler_OnClick(object sender, RoutedEventArgs e)
        {
            txtUtilisateur.Text = string.Empty; //Empty values
            txtMotPasse.Password = string.Empty; // ^
            
            MessageBox.Show("Vous avez cliquez sur le bouton Annuler.","Attention !", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }
    }
}