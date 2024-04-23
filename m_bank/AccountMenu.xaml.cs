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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace m_bank
{
    /// <summary>
    /// Interaction logic for AccountMenu.xaml
    /// </summary>
    public partial class AccountMenu : Window
    {
        SqlConnection con;
        public AccountMenu()
        {
            InitializeComponent();
            string connectionString = "  Data Source = labG9AEB3\\SQLEXPRESS; Initial Catalog = Bank; Integrated Security = True; Encrypt = True; Trust Server Certificate = True";
            con = new SqlConnection(connectionString);
            con.Open();
        }

       

        private void btnBalance_Click(object sender, RoutedEventArgs e)
        {
          /*  try
            {
                string queryy = "SELECT*FROM ACCOUNT";

                SqlCommand scom = new SqlCommand(queryy, con);//LINKING THE DATABASE CONNECTION WITH THE SQL QUERY
                using (SqlDataReader reader = scom.ExecuteReader())
                {
                    //ALLOWS US TO READ DATA FROM THE DATABASE THROUGH THE SQL COMMAND
                    while (reader.Read())
                    {
                      //  string display = "Balance :R" + reader.GetValue(0)
                         //   ;
                        string display = "Balance R" + reader["Balance"].ToString();

                        // lstDisplay.Items.Add(display);
                        MessageBox.Show(display);

                    }
                }
            }
            catch (Exception UnhandledError)
            {
                MessageBox.Show("Error while reading from list: " + UnhandledError.Message);
            }
            */
            
            Balance balance = new Balance();
            balance.Show();

        }

        //Exit Method
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnDeposit_Click(object sender, RoutedEventArgs e)
        {
             Deposit mydepo = new Deposit();
           mydepo.Show();
        }

        private void btnWithdraw_Click(object sender, RoutedEventArgs e)
        {
            Withdraw amount = new Withdraw();
            amount.Show();
        }

       
        private void btnAccRegister_Click(object sender, RoutedEventArgs e)
        {
            AccRegisterBank myacc = new AccRegisterBank();
            myacc.Show();
        }
    }
}
