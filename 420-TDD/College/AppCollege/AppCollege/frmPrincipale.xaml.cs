using System;
using System.Windows;
using System.Windows.Controls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace AppCollege
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection connexion;
        SqlDataAdapter da;
        DataSet dsCollege = new DataSet();
        DataRow enregistrement;
        bool nouveau = false;
        bool infoTexte = false;
        bool selection = false;
        int statut = -1;

        public MainWindow()
        {
            InitializeComponent();
            connexion = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        }

        private void frmPrincipale1_Loaded(object sender, RoutedEventArgs e)
        {
            da = new SqlDataAdapter("sp_SelectProgrammes", connexion);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.FillSchema(dsCollege, SchemaType.Mapped, "tblProgrammes");
            da.Fill(dsCollege, "tblProgrammes");

            da = new SqlDataAdapter("sp_SelectUtilisateurs", connexion);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.FillSchema(dsCollege, SchemaType.Mapped, "tblUtilisateurs");
            da.Fill(dsCollege, "tblUtilisateurs");

            da = new SqlDataAdapter("sp_SelectEtudiants", connexion);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.FillSchema(dsCollege, SchemaType.Mapped, "tblEtudiants");
            da.Fill(dsCollege, "tblEtudiants");
            
            dsCollege.Tables["tblEtudiants"].Columns["Nom_Complet"].ReadOnly = false;
            
            ListeEtudiants.ItemsSource = dsCollege.Tables["tblEtudiants"].DefaultView;
            ListeInstructeur.ItemsSource = dsCollege.Tables["tblUtilisateurs"].DefaultView;
            ListeProgramme.ItemsSource = dsCollege.Tables["tblProgrammes"].DefaultView;
        }

        private void ListeEtudiants_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListeEtudiants.SelectedIndex != -1)
            {
                enregistrement = dsCollege.Tables["tblEtudiants"].Rows.Find(ListeEtudiants.SelectedValue.ToString());

                txtID.Text = enregistrement["IdEtudiant"].ToString();
                txtPrenom.Text = enregistrement["Prenom"].ToString();
                txtNom.Text = enregistrement["Nom"].ToString();
                txtAdresse.Text = enregistrement["Adresse"].ToString();
                txtVille.Text = enregistrement["Ville"].ToString();
                txtProvince.Text = enregistrement["Province"].ToString();
                txtCodePostal.Text = enregistrement["CodePostal"].ToString();
                txtTelephone.Text = enregistrement["Telephone"].ToString();

                ListeInstructeur.SelectedValue = enregistrement["IdInstructeur"];
                ListeProgramme.SelectedValue = enregistrement["CodeProgramme"];

                switch (Convert.ToInt16(enregistrement["Statut"]))
                {
                    case -1:
                        rbActif.IsChecked = false;
                        rbArret.IsChecked = false;
                        rbGradue.IsChecked = false;
                        break;
                    case 0:
                        rbActif.IsChecked = true;
                        break;
                    case 1:
                        rbArret.IsChecked = true;
                        break;
                    case 2:
                        rbGradue.IsChecked = true;
                        break;
                }
            }
        }

        private void BtnNouveau_OnClick(object sender, RoutedEventArgs e)
        {
            Reset();
            
            ListeEtudiants.SelectedIndex = -1;
            ListeEtudiants.IsEnabled = false;
            txtID.IsReadOnly = false;
            btnNouveau.IsEnabled = false;
            nouveau = true;
        }

        private void Reset()
        {

            ListeEtudiants.SelectedIndex = -1;
            ListeEtudiants.IsEnabled = true;

            txtID.Text = txtPrenom.Text = txtNom.Text = txtAdresse.Text =
                txtVille.Text = txtProvince.Text = txtCodePostal.Text =
                    txtTelephone.Text = string.Empty;

            txtID.IsReadOnly = true;

            ListeInstructeur.SelectedIndex = -1;
            ListeProgramme.SelectedIndex = -1;

            rbActif.IsChecked = rbActif.IsChecked = rbGradue.IsChecked = false;

            btnNouveau.IsEnabled = true;

            btnEnregistrer.IsEnabled = false;
            btnSupprimer.IsEnabled = false;
            nouveau = false;
            statut = -1;
        }

        private void Verifier_Info(object sender, TextChangedEventArgs e)
        {
            if (nouveau)
            {
                if (!string.IsNullOrEmpty(txtID.Text.Trim()) &&
                    !string.IsNullOrEmpty(txtPrenom.Text.Trim()) &&
                    !string.IsNullOrEmpty(txtNom.Text.Trim()) &&
                    !string.IsNullOrEmpty(txtAdresse.Text.Trim()) &&
                    !string.IsNullOrEmpty(txtVille.Text.Trim()) &&
                    !string.IsNullOrEmpty(txtProvince.Text.Trim()) &&
                    !string.IsNullOrEmpty(txtCodePostal.Text.Trim()) &&
                    !string.IsNullOrEmpty(txtTelephone.Text.Trim()))
                {
                    infoTexte = true;
                    if (selection)
                    {
                        btnEnregistrer.IsEnabled = true;
                    }
                    if (!nouveau)
                    {
                        btnSupprimer.IsEnabled = true;
                    }
                }
            }
            else
            {
                btnEnregistrer.IsEnabled = true;
            }
        }

        private void Verifier_Selection(object sender, SelectionChangedEventArgs e)
        {
            if (nouveau)
            {
                if (ListeInstructeur.SelectedIndex != -1 &&
                    ListeProgramme.SelectedIndex != -1)
                {
                    selection = true;
                    if (infoTexte)
                    {
                        btnEnregistrer.IsEnabled = true;
                    }
                    if (!nouveau)
                    {
                        btnSupprimer.IsEnabled = true;
                    }
                }
            }
            else
            {
                btnEnregistrer.IsEnabled = true;
            }
        }

        private void BtnAnnuler_OnClick(object sender, RoutedEventArgs e)
        {
            Reset();
        }

        private void BtnEnregistrer_OnClick(object sender, RoutedEventArgs e)
        {

            if (nouveau)
            {
                try
                {
                    enregistrement = dsCollege.Tables["tblEtudiants"].NewRow();

                    enregistrement["IdEtudiant"] = txtID.Text;
                    enregistrement["Prenom"] = txtPrenom.Text;
                    enregistrement["Nom"] = txtNom.Text;
                    enregistrement["Adresse"] = txtAdresse.Text;
                    enregistrement["Ville"] = txtVille.Text;
                    enregistrement["Province"] = txtProvince.Text;
                    enregistrement["CodePostal"] = txtCodePostal.Text;
                    enregistrement["Telephone"] = txtTelephone.Text;
                    enregistrement["IdInstructeur"] = ListeInstructeur.SelectedValue;
                    enregistrement["CodeProgramme"] = ListeProgramme.SelectedValue;
                    enregistrement["Statut"] = statut;
                    enregistrement["Nom_Complet"] = txtPrenom.Text + " " + txtNom.Text;

                    dsCollege.Tables["tblEtudiants"].Rows.Add(enregistrement);

                    MessageBox.Show("Enregistrement du nouvel étudiant réussi.",
                        "Enregistrement", MessageBoxButton.OK,
                        MessageBoxImage.Information);
                    Reset();
                }

                catch (Exception ex)
                {
                    //MessageBox.Show("Le numéro d'étudiant existe déjà dans la table des étudiants.", "Erreur", MessageBoxButton.OK,
                    //MessageBoxImage.Error);
                    MessageBox.Show(ex.Message, "Erreur", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                    txtID.SelectAll();
                    txtID.Focus();
                }

            }
            else
            {
                enregistrement = dsCollege.Tables["tblEtudiants"].Rows.Find(txtID.Text);
                
                enregistrement["IdEtudiant"] = txtID.Text;
                enregistrement["Prenom"] = txtPrenom.Text;
                enregistrement["Nom"] = txtNom.Text;
                enregistrement["Adresse"] = txtAdresse.Text;
                enregistrement["Ville"] = txtVille.Text;
                enregistrement["Province"] = txtProvince.Text;
                enregistrement["CodePostal"] = txtCodePostal.Text;
                enregistrement["Telephone"] = txtTelephone.Text;
                enregistrement["IdInstructeur"] = ListeInstructeur.SelectedValue;
                enregistrement["CodeProgramme"] = ListeProgramme.SelectedValue;
                enregistrement["Statut"] = statut;
                enregistrement["Nom_Complet"] = txtPrenom.Text + " " + txtNom.Text;
                
                MessageBox.Show("Modification des informations de l'étudiant effectuée.", "Modification", MessageBoxButton.OK,
                MessageBoxImage.Information);
                Reset();
            }
        }

        private void BoutonRadio_Checked(object sender, RoutedEventArgs e)
        {
            switch (((RadioButton) sender).Name)
            {
                case "rbActif":
                    statut = 0;
                    break;
                case "rbArret":
                    statut = 1;
                    break;
                case "rbGradue":
                    statut = 2;
                    break;
            }
        }

        private void BtnSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Êtes-vous certain de vouloir supprimer cet étudiant ?",
                "Avertissement", MessageBoxButton.YesNo,
                MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                enregistrement = dsCollege.Tables["tblEtudiants"].Rows.Find(txtID.Text);

                dsCollege.Tables["tblEtudiants"].Rows.Remove(enregistrement);

                MessageBox.Show("Suppression de l'étudiant effectuée.", "Suppression",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Annulation de la suppression de l'étudiant.",
                    "Annulation", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            Reset();
        }
    }
}