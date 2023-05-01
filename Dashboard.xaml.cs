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
using Microsoft.Data.SqlClient;

namespace AP_ASSIGNMENT4
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        public Dashboard()
        {
            InitializeComponent();
            filldatagrid();
        }

        SqlConnection con = new SqlConnection(@"Data Source=localhost\sqlexpress01;Initial Catalog=AdvProg;Integrated Security=True;Trust Server Certificate=True;");

        private void filldatagrid()
        {
            string query = "SELECT Id, MakeId, Color, PetName FROM Inventory ORDER BY Id, MakeId Asc";

            try
            {
                con.Open();

                SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable("Inventory");
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

        private void Button_update_Click(object sender, RoutedEventArgs e)
        {
            string query = "UPDATE Inventory SET MakeId = @makeId, Color = @color, PetName = @petName WHERE Id = @id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", txtID.Text);
            cmd.Parameters.AddWithValue("@makeId", txtmake.Text);
            cmd.Parameters.AddWithValue("@color", txtColor.Text);
            cmd.Parameters.AddWithValue("@petName", txtPet.Text);

            try
            {
                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Record updated successfully.");
                    con.Close();
                    filldatagrid();
                }
                else
                {
                    MessageBox.Show("No rows updated.");
                }
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

        private void btndelete_Click(object sender, RoutedEventArgs e)
        {
            //con.Open();
            //string query = "DELETE FROM Orders WHERE Id = @id";
            //SqlCommand cmd = new SqlCommand(query, con);
            //cmd.Parameters.AddWithValue("@id", txtID.Text);
            //cmd.ExecuteNonQuery();
            //con.Close();

            string query = "DELETE FROM Orders WHERE Id = @id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", txtID.Text);
            
            try
            {
                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Record deleted successfully.");
                    con.Close();
                    con.Open();
                    string query_one = "DELETE FROM Inventory WHERE Id = @id";
                    SqlCommand cmd1 = new SqlCommand(query_one, con);
                    cmd1.Parameters.AddWithValue("@id", txtID.Text);
                    cmd1.ExecuteNonQuery();
                    con.Close();
                    filldatagrid();
                }
                else
                {
                    MessageBox.Show("No rows deleted.");
                }
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

        private void btnOrder_Click(object sender, RoutedEventArgs e)
        {
            con.Close();
            Order od = new Order();
            od.Show();
        }
    }
}
