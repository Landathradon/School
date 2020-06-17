using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace InjectionSQL
{
    public partial class frmInjectionSQL : Form
    {
        SqlConnection connexion;
        SqlCommand commande;

        public frmInjectionSQL()
        {
            InitializeComponent();
            connexion = new SqlConnection("server=JEAN-MAX-OMEN\\SQLEXPRESS; initial catalog=TestSQL; integrated security=True ");
        }

        private void btnRecherche_Click(object sender, EventArgs e)
        {
            try
            {
                txtDepartement.Text = string.Empty;
                // Création de notre requête SELECT.
                string recherche = "SELECT * FROM tblDepartements WHERE ID = @deptId";
                // Création de notre objet SqlCommande.
                commande = new SqlCommand(recherche, connexion);
                commande.Parameters.Add("@deptId", SqlDbType.TinyInt);
                commande.Parameters["@deptId"].Value = txtCode.Text;
                // Ouverture de notre connexion.
                connexion.Open();
                // Exécution de notre requête.
                SqlDataReader lecteur = commande.ExecuteReader();
                if (lecteur.Read())
                {
                    txtDepartement.Text = lecteur["NomDepartement"].ToString();
                }
                
                else
                {
                    
                    MessageBox.Show("Ce numéro de département n'existe pas.");
                    
                    txtCode.Text = string.Empty;
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

        private void btnEnregistrer_Click(object sender, EventArgs e)
        {
            try
            {
                string recherche = "SELECT ID FROM tblDepartements WHERE ID = @deptId";

                commande = new SqlCommand(recherche, connexion);
                commande.Parameters.Add("@deptId", SqlDbType.TinyInt);
                commande.Parameters["@deptId"].Value = txtCode.Text;
                
                connexion.Open();
                var resultat = commande.ExecuteScalar();
                if (resultat != null)
                {
                    MessageBox.Show("Ce numéro de département existe déjà dans la table.", "Enregistrement", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    string ligne = "INSERT INTO tblDepartements(ID, NomDepartement) VALUES (@deptID, @nomDept)";
                    commande = new SqlCommand(ligne, connexion);

                    commande.Parameters.Add("@deptId", SqlDbType.TinyInt);
                    commande.Parameters.Add("@nomDept", SqlDbType.VarChar);

                    commande.Parameters["@deptId"].Value = txtCode.Text;
                    commande.Parameters["@nomDept"].Value = txtDepartement.Text;

                    commande.ExecuteNonQuery();

                    MessageBox.Show("Enregistrement des données réussi", "Enregistrement", MessageBoxButtons.OK, MessageBoxIcon.Information);
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