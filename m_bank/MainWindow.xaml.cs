using Microsoft.Data.SqlClient;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace m_bank
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection con;
        public MainWindow()
        {

            InitializeComponent();
            // string connectionString = "Data Source=labG9AEB3\\SQLEXPRESS;Initial Catalog=Bank;Integrated Security=True; TrustServerCertificate = True";
            string connectionString="  Data Source = labG9AEB3\\SQLEXPRESS; Initial Catalog = Bank; Integrated Security = True; Encrypt = True; Trust Server Certificate = True";
            con = new SqlConnection(connectionString);
            con.Open();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string check = txtUsername.Text;

            string enteredPassword = txtPassword.Text; // the password entered by the user in the login form
            if (string.IsNullOrEmpty(check) || string.IsNullOrEmpty(enteredPassword))
            {
                MessageBox.Show("enter username or correct password");
            }
            else
            {


                // Retrieve the stored hashed password from the database
                string query = "SELECT Password FROM REGISTER WHERE Username = @Username";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Username", txtUsername.Text); // the username entered by the user in the login form

                    string storedHashedPassword = (string)cmd.ExecuteScalar();

                    // Compare the entered password with the stored hash
                    string enteredHashedPassword = HashPassword(enteredPassword);
                    if (enteredHashedPassword == storedHashedPassword)
                    {
                        MessageBox.Show("Connected");//To display message to the user

                        //To Clear the TextField
                        txtUsername.Text = "";
                        txtPassword.Text = "";

                        //Method used to access another window
                          AccountMenu myselect = new AccountMenu();
                         myselect.Show();

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

        }
        //Method for hashing password
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
             Register myselect = new Register();
             myselect.Show();
        }

        private void btnForgotPass_Click(object sender, RoutedEventArgs e)
        {
            ForgotPassword select = new ForgotPassword();
            select.Show();
        }
    }
}