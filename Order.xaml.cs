using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace AP_ASSIGNMENT4
{
    /// <summary>
    /// Interaction logic for Order.xaml
    /// </summary>
    public partial class Order : Window
    {
        public Order()
        {
            InitializeComponent();
            filldatagrid();
        }

        SqlConnection con = new SqlConnection(@"Data Source=localhost\sqlexpress01;Initial Catalog=AdvProg;Integrated Security=True;Trust Server Certificate=True;");

        private void filldatagrid()
        {
            string query = "SELECT Id, CustomerId, CarId FROM Orders";

            try
            {
                con.Open();

                SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable("Orders");
                adapter.Fill(dt);
                dtname.ItemsSource = dt.DefaultView;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
    }
}
