using System;
using System.Windows;
using System.ComponentModel;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;

namespace SuiviEtudiants
{
    public partial class SuiviEtudiantsUI : Window
    {
        private bool ouverture = false;
        private SqlConnection connexion;
        private UtilisateurActif utilisateur;
        private SqlCommand commande;
        private Etudiant etudiant;
        List<Etudiant> etudiants = new List<Etudiant>();

        public SuiviEtudiantsUI(UtilisateurActif actif)
        {
            InitializeComponent();
            utilisateur = actif;
            Title += utilisateur.Prenom + " " + utilisateur.Nom;
            connexion = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);

            EmplirListeEtudiants();
            ouverture = true;
        }

        private void EmplirListeEtudiants()
        {

            string selectEtudiant = "SELECT IdEtudiant, Prenom, Nom FROM tblEtudiants WHERE IdInstructeur = '" +
                                    utilisateur.IdUtilisateur + "' ORDER BY Nom";
            try
            {
                commande = new SqlCommand(selectEtudiant, connexion);
                connexion.Open();
                SqlDataReader lecteur = commande.ExecuteReader();

                while (lecteur.Read())
                {
                    etudiant = new Etudiant();
                    etudiant.IdEtudiant = lecteur["IdEtudiant"].ToString();
                    etudiant.Prenom = lecteur["Prenom"].ToString();
                    etudiant.Nom = lecteur["Nom"].ToString();
                    etudiants.Add(etudiant);
                }

                ListeEtudiants.DataContext = etudiants.ToList();
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

        private bool VerifierSaisie()
        {
            bool OK = !(txtPrenom.Text.Trim() == string.Empty || txtNom.Text.Trim() ==
                        string.Empty || txtAdresse.Text.Trim() == string.Empty ||
                        txtVille.Text.Trim() == string.Empty || txtProvince.Text.Trim() ==
                        string.Empty || txtCodePostal.Text.Trim() == string.Empty ||
                        txtTelephone.Text.Trim() == string.Empty);
            return OK;
        }

        private void ListeEtudiants_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListeEtudiants.SelectedIndex == -1)
            {
                return;
            }

            Etudiant sEtudiant = etudiants[ListeEtudiants.SelectedIndex];
            string selectEtudiant = "SELECT * FROM tblEtudiants WHERE IdEtudiant = '" + sEtudiant.IdEtudiant + "'";

            commande = new SqlCommand(selectEtudiant, connexion);
            connexion.Open();

            SqlDataReader lecteur = commande.ExecuteReader();

            if (lecteur.Read())
            {
                lblID.Content = lecteur["IdEtudiant"];
                txtPrenom.Text = lecteur["Prenom"].ToString();
                txtNom.Text = lecteur["Nom"].ToString();
                txtAdresse.Text = lecteur["Adresse"].ToString();
                txtVille.Text = lecteur["Ville"].ToString();
                txtProvince.Text = lecteur["Province"].ToString();
                txtCodePostal.Text = lecteur["CodePostal"].ToString();
                txtTelephone.Text = lecteur["Telephone"].ToString();

                switch (lecteur["Statut"].ToString())
                {
                    case "0":
                        rbActif.IsChecked = true;
                        break;
                    case "1":
                        rbArret.IsChecked = true;
                        break;
                    case "2":
                        rbGradue.IsChecked = true;
                        break;
                }
            }

            connexion.Close();
        }

        private void mnuAjouterEtudiant_Click(object sender, RoutedEventArgs e)
        {
            frmNouvelEtudiant ajoutEtudiant = new frmNouvelEtudiant();

            ajoutEtudiant.ShowDialog();

            etudiants.Clear();

            EmplirListeEtudiants();
        }

        private void mnuQuitter_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnEnregistrer_OnClick(object sender, RoutedEventArgs e)
        {
            int statut = -1;

            statut = (rbActif.IsChecked == true ? 0 :
                rbArret.IsChecked == true ? 1 :
                rbGradue.IsChecked == true ? 2 : -1);

            if (statut != -1)
            {
                if (VerifierSaisie())
                {
                    try
                    {
                        connexion.Open();
                        try
                        {

                            string enregistrerEtudiant =
                                "UPDATE tblEtudiants SET Prenom = '" + txtPrenom.Text + "', Nom = '" + txtNom.Text +
                                "', Adresse = '" +
                                txtAdresse.Text + "', Ville = '" + txtVille.Text +
                                txtProvince.Text + "', CodePostal = '" +
                                txtCodePostal.Text + "',Telephone = '" + txtTelephone.Text + "', Statut = '" + statut +
                                "' WHERE IdEtudiant = '" + lblID.Content + "'";
                            commande = new SqlCommand(enregistrerEtudiant, connexion);

                            commande.ExecuteNonQuery();
                            MessageBox.Show("Mise-à-jour réussie.", "Information", MessageBoxButton.OK,
                                MessageBoxImage.Information);
                        }
                        catch
                        {
                            MessageBox.Show(
                                "La province de résidence de l'étudiant doit être saisie sur deux caractères, Qc par exemple.",
                                "Attention", MessageBoxButton.OK,
                                MessageBoxImage.Information);
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
                    MessageBox.Show("Vous devez saisir toutes les informations requises.", "Attention !",
                        MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }

            }
            else
            {
                MessageBox.Show(
                    "Vous devez sélectionner le statut de l'étudiant" + Environment.NewLine +
                    "L'enregistrement des données ne sera pas effectué.", "Attention !", MessageBoxButton.OK,
                    MessageBoxImage.Exclamation);
            }

        }

        private void BtnSupprimer_OnClick(object sender, RoutedEventArgs e)
        {
            if (ListeEtudiants.SelectedIndex != -1)
            {
                if (MessageBox.Show(
                    $"Désirez-vous réellement supprimer l'enregistrement de {txtPrenom.Text} {txtNom.Text} ?" +
                    Environment.NewLine +
                    "Cette opération est irréversible.", "Attention", MessageBoxButton.YesNo,
                    MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        connexion.Open();

                        string suppression = $"DELETE FROM tblEtudiants WHERE IdEtudiant = ' {lblID.Content.ToString()} '";

                        SqlCommand commande = new SqlCommand(suppression, connexion);

                        int ligne = commande.ExecuteNonQuery();
                        if (ligne != 0)
                        {
                            MessageBox.Show($"Suppression de {txtPrenom.Text} {txtNom.Text} réussie.");
                            
                            connexion.Close();

                            etudiants.Clear();

                            EmplirListeEtudiants();

                            lblID.Content = "";
                            txtPrenom.Text = txtNom.Text = txtAdresse.Text = string.Empty;
                            txtVille.Text = txtProvince.Text = string.Empty;
                            txtCodePostal.Text = txtTelephone.Text = string.Empty;
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
            }
        }
    }
}