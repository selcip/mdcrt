using MySql.Data.MySqlClient;
using Squirrel;
using System;
using System.Xml;
using System.Windows;
using System.IO;
using Newtonsoft.Json.Linq;

namespace mdctr_metro
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        MySqlConnection conn = new MySqlConnection("Server=localhost;userid=root;password=dinhero123;Database=madrecita");

        public MainWindow()
        {
            InitializeComponent();
            createConfig();
            //update();
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Console.WriteLine(senha.Password);
            Console.WriteLine(user.Text);
            try
            {
                conn.Open();
                string query = $"SELECT * FROM usuarios WHERE usuario = '{user.Text}' AND senha = '{senha.Password}'";
                Console.WriteLine(query);
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader dtreader = cmd.ExecuteReader();

                int rowcount = 0;
                while (dtreader.Read())
                {
                    rowcount++;
                }

                if (rowcount > 0)
                {
                    Container principal = new Container();
                    principal.Show();
                    this.Close();
                } 
                else
                {
                    MessageBox.Show("Usuário ou senha não existentes");
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

        async static void update()
        {
            using (var mgr = UpdateManager.GitHubUpdateManager("https://github.com/selcip/mdcrt"))
            {
                await mgr.Result.UpdateApp();
            }
        }

        private void createConfig()
        {
            JObject o1 = JObject.Parse(File.ReadAllText(@"config.json"));
            Global.connection = $"Server={o1["host"]};userid={o1["usuario"]};password={o1["senha"]};Database={o1["db"]}";
        }
    }
}
