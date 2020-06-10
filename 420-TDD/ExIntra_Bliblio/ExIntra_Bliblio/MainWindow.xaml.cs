using System;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;

namespace ExIntra_Bliblio
{

    public partial class MainWindow
    {
        private SqlConnection connexion;
        private SqlCommand commande;
        
        public MainWindow()
        {
            InitializeComponent();
            ResearchBox.Focus();
            connexion = new SqlConnection("server=JEAN-MAX-OMEN\\SQLEXPRESS; initial catalog=biblio; integrated security= true");
        }

        private void ResearchBtn_OnClick(object sender, RoutedEventArgs e)
        {
            string findBook = $"SELECT * FROM Livres WHERE Titre like '%{ResearchBox.Text}%'";
            AddBooksToList(findBook);
        }

        private void ShowAllBooks_OnClick(object sender, RoutedEventArgs e)
        {
            
            string findBook = "SELECT * FROM Livres";
            AddBooksToList(findBook);
        }
        
        private void ResearchBox_OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string findBook = $"SELECT * FROM Livres WHERE Titre like '%{ResearchBox.Text}%'";
                AddBooksToList(findBook);
            }
        }

        private void AddBooksToList(string findBook)
        {
            ResearchListBox.Items.Clear();
            commande = new SqlCommand(findBook, connexion);
            try
            {
                connexion.Open();

                SqlDataReader lecteur = commande.ExecuteReader();   
                while (lecteur.Read())
                {
                    string book = lecteur["Titre"].ToString();
                    ResearchListBox.Items.Add(book);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Il y a eu une erreur {Environment.NewLine}{ex}","Attention !", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            finally
            {
                connexion.Close();
            } 
        }
    }
}