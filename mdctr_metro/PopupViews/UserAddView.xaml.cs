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
    /// Lógica interna para UserAddView.xaml
    /// </summary>
    public partial class UserAddView
    {
        MySqlConnection conn = new MySqlConnection("Server=localhost;userid=root;password=dinhero123;Database=madrecita");

        public UserAddView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                conn.Open();
                string query = $"INSERT INTO usuarios VALUES(NULL, '{user.Text}', '{nome.Text}', '{senha.Password}', '{acesso.Text}', 0, DEFAULT, DEFAULT)";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Usuário cadastrado!");
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
    }
}
