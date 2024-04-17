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
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        SqlConnection con;
        public Register()
        {
            InitializeComponent();
            string connectionString = "  Data Source = labG9AEB3\\SQLEXPRESS; Initial Catalog = Bank; Integrated Security = True; Encrypt = True; Trust Server Certificate = True";
            con = new SqlConnection(connectionString);
            con.Open();
        }
        public static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedPasswordBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedPasswordBytes);
            }
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            string check = txtUsernameReg.Text;
            string password = txtPasswordReg.Text; // the password entered by the user in the registration form
            string hashedPassword = HashPassword(password);

            if (string.IsNullOrEmpty(check) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Complete all fields");
            }
            else
            {


                // Store the hashed password in the database
                string query = "INSERT INTO REGISTER ( Username, Password) VALUES ( @Username, @Password)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    try
                    {


                        
                        cmd.Parameters.AddWithValue("@Username", txtUsernameReg.Text); // the username entered by the user in the registration form
                        cmd.Parameters.AddWithValue("@Password", hashedPassword);
                        // add other parameters...

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Successfully Registered");//To Display Message To user
                       
                        //To Clear text field
                        txtUsernameReg.Text = "";
                        txtPasswordReg.Text = "";

                        //Method used to Access another window.
                       AccountMenu BankS = new AccountMenu();
                        BankS.Show();
                        
                        //To Close Window after registering
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("error");
                    }
                }
            }
        }
    }
}
