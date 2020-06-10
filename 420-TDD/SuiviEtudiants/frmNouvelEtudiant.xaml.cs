using System;
using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
using System.Configuration;

namespace SuiviEtudiants
{
    public partial class frmNouvelEtudiant : Window
    {
        private SqlConnection connexion;
        private SqlCommand commande;

        private string idInstructeur, idProgramme;

        public frmNouvelEtudiant()
        {
            InitializeComponent();
            connexion = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
            cmbInstructeur.Items.Add("Yves Desharnais");
            cmbInstructeur.Items.Add("Michel Leduc");
            cmbProgramme.Items.Add("Programmeur-analyste - orienté Internet");
            cmbProgramme.Items.Add("Gestionnaire en réseautique - spécialiste de la sécurité");

        }

        private bool VerifierSaisie()
        {
            bool OK = !(txtID.Text.Trim() == string.Empty || txtPrenom.Text.Trim() == string.Empty ||
                        txtNom.Text.Trim() == string.Empty || txtAdresse.Text.Trim() == string.Empty
                        || txtVille.Text.Trim() == string.Empty || txtProvince.Text.Trim() == string.Empty ||
                        txtCodePostal.Text.Trim() == string.Empty
                        || txtTelephone.Text.Trim() == string.Empty || cmbInstructeur.SelectedIndex == -1 ||
                        cmbProgramme.SelectedIndex == -1);
            return OK;
        }

        private void cmbInstructeur_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (cmbInstructeur.SelectedIndex)
            {
                case 0:
                    idInstructeur = "yd001";
                    break;
                case 1:
                    idInstructeur = "ml001";
                    break;
            }
        }

        private void cmbProgramme_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (cmbProgramme.SelectedIndex)
            {
                case 0:
                    idProgramme = "LEA.9C";
                    break;
                case 1:
                    idProgramme = "LEA.AE";
                    break;
            }
        }

        private void BtnEnregistrer_OnClick(object sender, RoutedEventArgs e)
        {
            bool OK = VerifierSaisie();

            if (OK)
            {
                // Création d'une requête sélection
                string verifie = $"SELECT IdEtudiant FROM tblEtudiants WHERE IdEtudiant= '{ txtID.Text}'";

                commande = new SqlCommand(verifie, connexion);
                try
                {
                    connexion.Open();

                    SqlDataReader lecteur = commande.ExecuteReader();
                    if(lecteur.Read())
                    {
                        MessageBox.Show("Ce numéro d'identification est déjà utilisé dans la table.", "Attention !", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                    else
                    {
                        lecteur.Close();
                        
                        string insereEtudiant = $"INSERT INTO tblEtudiants(IdEtudiant, Prenom,Nom, Adresse, Ville, Province, CodePostal,Telephone, CodeProgramme, " +
                                                $"IdInstructeur)VALUES('{txtID.Text}', '{txtPrenom.Text}','{txtNom.Text}', '{txtAdresse.Text}','{txtVille.Text}', " +
                                                $"'{txtProvince.Text}','{txtCodePostal.Text}', '{txtTelephone.Text}','{idProgramme}', '{idInstructeur}')";

                        commande = new SqlCommand(insereEtudiant, connexion);
                        try
                        {
                            connexion.Open();
                            int ligne = commande.ExecuteNonQuery();

                            if (ligne != 0)
                            {
                                if (MessageBox.Show("Enregistrement du nouvel étudiant réussi." + Environment.NewLine +
                                                                 "Désirez-vous enregistrer un autre étudiant ?",
                                                          "Enregistrement réussi", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                                {
                                    Close();
                                }
                                else
                                {
                                    txtID.Text = txtPrenom.Text = string.Empty;
                                    txtNom.Text = txtAdresse.Text = string.Empty;
                                    txtVille.Text = txtProvince.Text = string.Empty;
                                    txtCodePostal.Text = txtTelephone.Text = string.Empty;
                                    cmbInstructeur.SelectedIndex = -1;
                                    cmbProgramme.SelectedIndex = -1;
                                    txtID.Focus();
                                }
                            }
                        }
                        catch (Exception er)
                        {
                            MessageBox.Show("La province de résidence de l'étudiant doit être saisie sur deux caractères, Qc par exemple.","Attention",MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        finally
                        {
                            connexion.Close();
                        }

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
            else
            {
                MessageBox.Show("Vous devez saisir toutes les informations requises", "Attention !",
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
    }
}