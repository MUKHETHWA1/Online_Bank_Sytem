using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace m_bank
{
    /// <summary>
    /// Interaction logic for Balance.xaml
    /// </summary>
    public partial class Balance : Window
    {
        SqlConnection con;
        public Balance()
        {
            InitializeComponent();
            string connectionString = "  Data Source = labG9AEB3\\SQLEXPRESS; Initial Catalog = Bank; Integrated Security = True; Encrypt = True; Trust Server Certificate = True";
            con = new SqlConnection(connectionString);
            con.Open();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
           this.Close();
        }

        private void btnBalance_Click(object sender, RoutedEventArgs e)
        {
            try { 
            string query = $"SELECT * FROM ACCOUNT WHERE AccountID = '{txtID.Text}'";
            using (SqlCommand command = new SqlCommand(query, con))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Retrieve other relevant data fields (e.g., Name, Email) from the reader
                        string display = "Balance R" + reader["Balance"].ToString();


                        MessageBox.Show(display);
                    }
                    else
                    {
                        // Handle case when no record is found for the entered ID
                        MessageBox.Show("No balance/record found, Go to Option 4 To Register Account.");
                    }
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
