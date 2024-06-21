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
    /// Interaction logic for AdminPanel.xaml
    /// </summary>
    public partial class AdminPanel : Window
    {
        SqlConnection con;
        public AdminPanel()
        {
            InitializeComponent();
            string connectionString = "  Data Source = labG9AEB3\\SQLEXPRESS; Initial Catalog = Bank; Integrated Security = True; Encrypt = True; Trust Server Certificate = True";
            con = new SqlConnection(connectionString);
            con.Open();
        }

        private void btnAdminPanelSignOut_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnDisplay_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string queryy = "SELECT*FROM ACCOUNT;";

                SqlCommand scom = new SqlCommand(queryy, con);//LINKING THE DATABASE CONNECTION WITH THE SQL QUERY
                SqlDataReader reader = scom.ExecuteReader();//ALLOWS US TO READ DATA FROM THE DATABASE THROUGH THE SQL COMMAND
                while (reader.Read())
                {
                    string display = "Account ID:" + reader.GetValue(0) + "\n" +
                        "Account Balance: " + reader.GetValue(1) 
                        
                        ;
                    
                   lstDisplay.Items.Add(display);


                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("enter values");
            }
        }
    }
}
