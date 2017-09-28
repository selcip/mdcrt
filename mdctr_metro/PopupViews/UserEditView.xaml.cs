using MySql.Data.MySqlClient;
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

namespace mdctr_metro.PopupViews
{
    /// <summary>
    /// Lógica interna para UserEditView.xaml
    /// </summary>
    public partial class UserEditView
    {

        public string UserID { get; set; }
        MySqlConnection conn = new MySqlConnection("Server=localhost;userid=root;password=dinhero123;Database=madrecita");

        public UserEditView(string value)
        {
            InitializeComponent();
            this.UserID = value;
            fillForm();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                conn.Open();
                string query = $"UPDATE usuarios SET nome = '{nome.Text}', senha = '{senha.Password}', nivel = '{acesso.Text}', usuario = '{user.Text}' WHERE id = '{this.UserID}' ";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Salvo com sucesso!");
                this.Close();
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

        private void fillForm()
        {
            try
            {
                conn.Open();
                string query = $"SELECT * FROM usuarios WHERE id = {this.UserID}";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader dtreader = cmd.ExecuteReader();

                while (dtreader.Read())//Enquanto existir dados no select
                {
                    id.Text = dtreader["id"].ToString();
                    user.Text = dtreader["usuario"].ToString();
                    nome.Text = dtreader["nome"].ToString();
                    senha.Password = dtreader["senha"].ToString();
                    acesso.Text = dtreader["nivel"].ToString();
                }
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
    }
}
