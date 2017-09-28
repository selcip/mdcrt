using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using mdctr_metro.PopupViews;

namespace mdctr_metro.TabViews
{
    /// <summary>
    /// Interação lógica para UsuariosView.xam
    /// </summary>
    public partial class UsuariosView : UserControl
    {

        MySqlConnection conn = new MySqlConnection("Server=localhost;userid=root;password=dinhero123;Database=madrecita");

        public UsuariosView()
        {
            InitializeComponent();
            updateTable();            
        }   
        
        public void updateTable()
        {
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM usuarios WHERE deleted = 0", conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                DataTable data = new DataTable();
                adp.Fill(data);
                gridUsuarios.DataContext = data;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                conn.Close();
            }
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            updateTable();
        }

        private void myDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (gridUsuarios.SelectedItem == null)
                    return;
                DataRowView dr = gridUsuarios.SelectedItem as DataRowView;
                DataRow dr1 = dr.Row;

                UserEditView janelaEdicao = new UserEditView(Convert.ToString(dr1.ItemArray[0]));
                janelaEdicao.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UserAddView janelaAdd = new UserAddView();
            janelaAdd.ShowDialog();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var id = returnId();
            try
            {
                conn.Open();
                string query = $"UPDATE usuarios SET deleted = 1 WHERE id = {id}";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Usuário excluido!");
                updateTable();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                conn.Close();
            }
            Console.WriteLine(id);
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            UserEditView janelaEdicao = new UserEditView(returnId());
            janelaEdicao.ShowDialog();
        }

        private string returnId()
        {
            DataRowView rowview = gridUsuarios.SelectedItem as DataRowView;
            string id = rowview.Row[0].ToString();
            return id;
        }
    }
}
