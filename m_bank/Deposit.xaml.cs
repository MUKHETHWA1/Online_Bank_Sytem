using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
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
    /// Interaction logic for Deposit.xaml
    /// </summary>
    public partial class Deposit : Window
    {
        SqlConnection con;
        public Deposit()
        {
            InitializeComponent(); string connectionString = "  Data Source = labG9AEB3\\SQLEXPRESS; Initial Catalog = Bank; Integrated Security = True; Encrypt = True; Trust Server Certificate = True";
            con = new SqlConnection(connectionString);
            con.Open();

        }

        private void btnExitDep_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnDepositAmt_Click(object sender, RoutedEventArgs e)
        {
            // Assuming you have already established a valid SqlConnection 'con'
            try
            {
                // Get the new balance value from your WPF TextBox (txtAmount)
                int newBalance = int.Parse(txtAmount.Text); // Assuming txtAmount.Text contains a valid decimal value
                int accountId = int.Parse(txtAccIDd.Text);
                // Construct the SQL query with parameters
                string query = "UPDATE ACCOUNT SET Balance = Balance + @NewBalance WHERE AccountID = @AccountId";
                //string query = "UPDATE ACCOUNT SET Balance = Balance + @NewBalance ";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Add parameters
                    cmd.Parameters.AddWithValue("@NewBalance", newBalance);
                    cmd.Parameters.AddWithValue("@AccountId", accountId); // Replace 'accountId' with the actual account ID you want to update

                    try
                    {

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Amount deposited successfully!");
                        }
                        else
                        {
                            MessageBox.Show("No records were updated.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error updating balance: " + ex.Message);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
