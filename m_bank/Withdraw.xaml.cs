﻿using Microsoft.Data.SqlClient;
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
    /// Interaction logic for Withdraw.xaml
    /// </summary>
    public partial class Withdraw : Window
    {
        //connection for sql
        SqlConnection con;
        public Withdraw()
        {
            InitializeComponent();
             string connectionString = " Your Connection String";
            con = new SqlConnection(connectionString);
            con.Open();
        }

        //Exit button method
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //
        private void btnWithdrawAmt_Click(object sender, RoutedEventArgs e)
        {
            // Get the new balance value from your WPF TextBox (txtWithdraw)
            int newBalance = int.Parse(txtWithdraw.Text); // Assuming txtAmount.Text contains a valid decimal value
            int accountId = int.Parse(txtAccID.Text);
            // Construct the SQL query with parameters
             string query = "UPDATE ACCOUNT SET Balance = Balance - @NewBalance WHERE AccountID = @AccountId";
           // string query = "UPDATE ACCOUNT SET Balance = Balance - @NewBalance ";

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
                        MessageBox.Show("Amount Withdraw successfully!");
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
    }
}
