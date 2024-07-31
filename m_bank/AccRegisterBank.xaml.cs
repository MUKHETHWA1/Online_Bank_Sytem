using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for AccRegisterBank.xaml
    /// </summary>
    public partial class AccRegisterBank : Window
    {
        SqlConnection con;
        public AccRegisterBank()
        {
            //Connection to the sql database
            InitializeComponent();
            string connectionString = "  Data Source = labG9AEB3\\SQLEXPRESS; Initial Catalog = Bank; Integrated Security = True; Encrypt = True; Trust Server Certificate = True";
            con = new SqlConnection(connectionString);
            con.Open();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            //Exit method
            this.Close();
        }

        private void btnAccRegister_Click(object sender, RoutedEventArgs e)
        {
            // Store the hashed password in the database
            string query = "INSERT INTO ACCOUNT ( AccountID, Balance) VALUES ( @AccountID, @Balance)";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                try
                {

                    cmd.Parameters.AddWithValue("@AccountID", txtEnterAcc.Text); // the username entered by the user in the registration form
                    cmd.Parameters.AddWithValue("@Balance", txtEnterBalance.Text);
                    // add other parameters...

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Successfully Created");//To Display Message To user
                    //to clear texfields
                    txtEnterAcc.Text = "";
                    txtEnterBalance.Text = "";
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("error");
                }
            }

        }
    }
}
