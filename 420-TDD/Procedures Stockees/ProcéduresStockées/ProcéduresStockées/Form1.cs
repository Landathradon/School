using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace ProcéduresStockées
{
    public partial class Form1 : Form
    {
        SqlConnection connexion;

        public Form1()
        {
            InitializeComponent();
            connexion = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        }

        private void Reset()
        {
            txtCode.Text = txtDepartement.Text = string.Empty;
            btnAjouter.Text = "Ajouter";
            btnAjouter.Enabled = false;
            btnRechercher.Enabled = false;
            btnSupprimer.Enabled = false;
            txtCode.ReadOnly = false;
        }

        private string VerifierNomDept(string nomDept)
        {
            nomDept = nomDept.Trim();
            if (nomDept != string.Empty)
            {
                nomDept = txtDepartement.Text;
                nomDept = nomDept[0].ToString().ToUpper() +
                          nomDept.Substring(1).ToLower();
            }

            return nomDept;
        }

        private void txtCode_TextChanged(object sender, EventArgs e)
        {
            btnRechercher.Enabled = true;
            if (txtDepartement.Text.Trim() != string.Empty)
            {
                btnAjouter.Enabled = true;
            }
        }

        private void txtDepartement_TextChanged(object sender, EventArgs e)
        {
            if (txtCode.Text.Trim() != string.Empty)
            {
                btnAjouter.Enabled = true;
            }
        }

        private void AjouterDepartement(string nomDept)
        {
            try
            {
                connexion.Open();
                SqlCommand commande = new SqlCommand("spAjouterModifierDepartement", connexion);
                commande.CommandType = CommandType.StoredProcedure;

                commande.Parameters.AddWithValue("@Mode", btnAjouter.Text);
                commande.Parameters.AddWithValue("@IdDept", txtCode.Text);
                commande.Parameters.AddWithValue("@Dept", nomDept);

                SqlParameter retour = new SqlParameter("@Retour", SqlDbType.Int);
                retour.Direction = ParameterDirection.Output;
                commande.Parameters.Add(retour);

                commande.ExecuteNonQuery();

                int resultat = (int) retour.Value;
                switch (resultat)
                {
                    case 0:

                        MessageBox.Show("Ce numéro de département existe déjà dans la table.", "Erreur",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        txtCode.Text = string.Empty;
                        txtCode.Focus();
                        btnAjouter.Enabled = false;
                        break;
                    case 1:
                        MessageBox.Show("Ce nom de département existe déjà dans la table.", "Erreur",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        txtDepartement.Text = string.Empty;
                        txtDepartement.Focus();
                        btnAjouter.Enabled = false;
                        break;
                    case 2:
                        MessageBox.Show(
                            btnAjouter.Text == "Ajouter"
                                ? "Enregistrement du nouveau département réussi."
                                : "Mide-à-jour réussie.", "Enregistrement", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        Reset();
                        break;
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

        private void btnRechercher_Click(object sender, EventArgs e)
        {
            uint codeRecherche;
            bool ok = uint.TryParse(txtCode.Text, out codeRecherche);
            if (ok)
            {
                try
                {
                    connexion.Open();

                    SqlCommand commande = new SqlCommand("spChercherDepartement", connexion);

                    commande.CommandType = CommandType.StoredProcedure;
                    commande.Parameters.AddWithValue("@IdDept", txtCode.Text);

                    SqlParameter retour = new SqlParameter("@Dept", SqlDbType.NVarChar, 50);
                    retour.Direction = ParameterDirection.Output;
                    commande.Parameters.Add(retour);

                    commande.ExecuteNonQuery();

                    string resultat = retour.Value.ToString();
                    if (resultat != string.Empty)
                    {
                        txtDepartement.Text = resultat;
                        txtCode.ReadOnly = true;
                        btnRechercher.Enabled = false;
                        btnSupprimer.Enabled = true;
                        btnAjouter.Text = "Modifier";
                        btnAjouter.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("Il n'y a aucun enregistrement pour ce numéro de code dans la table.", "Erreur",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);

                        Reset();
                        txtCode.Focus();
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
                MessageBox.Show("Le code saisi doit être une valeur numérique entière positive.", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Reset();
                txtCode.Focus();
            }

        }

        private void btnAjouter_Click(object sender, EventArgs e)
        {
            uint codeRecherche;
            string nomDept = string.Empty;
            bool ok = uint.TryParse(txtCode.Text, out codeRecherche);
            if (ok)
            {
                nomDept = VerifierNomDept(txtDepartement.Text);
                if (nomDept != String.Empty)
                {
                    AjouterDepartement(nomDept);
                }
                else
                {
                    MessageBox.Show("Vous devez saisir le nom du département.", "Erreur", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    txtDepartement.Text = String.Empty;
                    txtDepartement.Focus();
                    btnAjouter.Enabled = false;
                }
            }
            else
            {
                MessageBox.Show("Le code saisi doit être une valeur numérique entière positive.", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                Reset();
                txtCode.Focus();
            }
        }

        private void btnSupprimer_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Désirez-vous réellement supprimer cet enregistrement ?", "Suppression", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    connexion.Open();

                    SqlCommand commande = new SqlCommand("spSupprimerDepartement", connexion);

                    commande.CommandType = CommandType.StoredProcedure;
                    commande.Parameters.AddWithValue("@IdDept", txtCode.Text);

                    commande.ExecuteNonQuery();
                    MessageBox.Show("Suppression de l'enregistrement complétée.", "Suppression", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            Reset();
        }

        private void btnAnnuler_Click(object sender, EventArgs e)
        {
            Reset();
        }
    }
}
