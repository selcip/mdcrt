using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;
using System.Windows;
using mdctr_metro.TabViews;

namespace mdctr_metro
{
    /// <summary>
    /// Interaction logic for Container.xaml
    /// </summary>
    public partial class Container
    {
        MySqlConnection conn = new MySqlConnection("Server=localhost;userid=root;password=dinhero123;Database=madrecita");

        public Container()
        {
            InitializeComponent();
        }

        private void loadbtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM usuarios", conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds, "LoadDataBinding");
                gridvendas.DataContext = ds;
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
