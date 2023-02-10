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

using MySql.Data.MySqlClient;
using SqlConn;

namespace ComputerShop
{
    /// <summary>
    /// Логика взаимодействия для Authentification.xaml
    /// </summary>
    public partial class Authentification : Page
    {
        private MySqlConnection conn = DBUtils.GetDBConnection();

        public Authentification()
        {
            InitializeComponent();
            try
            {
                conn.Open();
            }

            catch (Exception e)
            {
                WarningMessage.Show("Ошибка! " + e);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string login = tb_loginInput.Text;
            string password = tb_passwordInput.Text;

            string query = "select access from authentification where login = '" + login + "' and password = '" + password + "'";
            MySqlCommand command = new MySqlCommand(query, conn);

            //try
            //{
                Manager.Access = command.ExecuteScalar().ToString();
                Manager.CurrentPageName = "Каталог";
                NavigationService.Navigate(new Catalog(conn));
            //}         
            //catch (Exception err)
            //{
                //WarningMessage.Show("Неверный логин или пароль!");
            //}
        }
    }
}
