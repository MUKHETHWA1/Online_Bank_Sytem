using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
    /// Interaction logic for ForgotPassword.xaml
    /// </summary>
    public partial class ForgotPassword : Window
    {
        SqlConnection con;
        public ForgotPassword()
        {
            InitializeComponent();
            string connectionString = "  Data Source = labG9AEB3\\SQLEXPRESS; Initial Catalog = Bank; Integrated Security = True; Encrypt = True; Trust Server Certificate = True";
            con = new SqlConnection(connectionString);
            con.Open();
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            string check = txtEnterUsername.Text;

            if (string.IsNullOrEmpty(check) )
            {
                MessageBox.Show("enter username or correct password");
            }
            else
            {


                // Retrieve the stored hashed password from the database
                string query = "SELECT Username FROM REGISTER WHERE Username = @Username";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Username", txtEnterUsername.Text); // the username entered by the user in the login form

                   
                    if (check == query)
                    {
                        MessageBox.Show("Connected");//To display message to the user

                        ChangePass changePass = new ChangePass();
                        changePass.Show();
                        this.Close();

                    }
                    else
                    {
                        // username do not match, show an error message
                        MessageBox.Show("Enter Correct Username");
                    }
                }
            }

            
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
