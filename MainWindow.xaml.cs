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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Data.SqlClient;

namespace AP_ASSIGNMENT4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection(@"Data Source = localhost\sqlexpress01; Initial Catalog=AdvProg; Integrated Security=True; Trust Server Certificate=True;");
            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                    string query = "Select Username, pass FROM Login WHERE [Username]=@username AND [pass]=@pass";
                    SqlCommand sqlCommand = new SqlCommand(query, connection);
                    sqlCommand.Parameters.AddWithValue("@username", txtBoxUsername.Text);
                    sqlCommand.Parameters.AddWithValue("@pass", txtBoxPassword.Password.ToString());
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    if (reader.HasRows)
                    {
                        Dashboard screen = new Dashboard();
                        screen.Title = "Dashboard Window";
                        screen.Show();
                        this.Close();
                        connection.Close();
                    }
                    else
                    {
                        MessageBox.Show("Username or Password is Incorrect!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { connection.Close(); }
        }

        private void txtBoxUsername_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
