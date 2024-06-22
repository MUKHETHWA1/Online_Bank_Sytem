using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace m_bank
{
    /// <summary>
    /// Interaction logic for Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        SqlConnection con; 
        public Admin()
        {
            InitializeComponent();
            string connectionString = "  Data Source = labG9AEB3\\SQLEXPRESS; Initial Catalog = Bank; Integrated Security = True; Encrypt = True; Trust Server Certificate = True";
            con = new SqlConnection(connectionString);
            con.Open();
        }

        private void btnAdminSignin_Click(object sender, RoutedEventArgs e)
        {
            string check = txtAdminUsername.Text;

            string enteredPassword = txtAdminPassword.Text; // the password entered by the user in the login form
            if (string.IsNullOrEmpty(check) || string.IsNullOrEmpty(enteredPassword))
            {
                MessageBox.Show("enter username or correct password");
            }
            else
            {


                // Retrieve the stored hashed password from the database
                string query = "SELECT Password FROM ADMIN WHERE Username = @Username";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Username", txtAdminUsername.Text); // the username entered by the user in the login form

                    string storedHashedPassword = (string)cmd.ExecuteScalar();

                    // Compare the entered password with the stored hash
                    string enteredHashedPassword = HashPassword(enteredPassword);
                    if (enteredHashedPassword == storedHashedPassword)
                    {
                        MessageBox.Show("Connected");//To display message to the user

                        //To Clear the TextField
                        txtAdminUsername.Text = "";
                        txtAdminPassword.Text = "";

                        //Method used to access another window
                        AdminPanel panel = new AdminPanel();
                        panel.Show();

                        //To Close Window After user has Login
                        this.Close();

                    }
                    else
                    {
                        // Passwords do not match, show an error message
                        MessageBox.Show("CREATE AN ACCOUNT OR ENTER CORRECT DETAILS", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            /* AdminPanel panel = new AdminPanel();
             panel.Show();
             this.Close();*/
        }
        public static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedPasswordBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedPasswordBytes);
            }
        }
    }
}
