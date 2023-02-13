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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ComputerShop
{
    public partial class Catalog : Page
    {
        MySqlConnection conn;
        public Catalog(MySqlConnection conn)
        {
            InitializeComponent();
            this.conn = conn;
            CatalogItemFrame.Navigate(new ProcessorsPage(conn));
        }

        private void Btn_Processors_Click(object sender, RoutedEventArgs e)
        {
            Manager.CurrentPageName = "Процессоры";
            CatalogItemFrame.Navigate(new ProcessorsPage(conn));
        }

        private void Btn_RAM_Click(object sender, RoutedEventArgs e)
        {
            Manager.CurrentPageName = "Оперативная память";
            //CatalogItemFrame.Navigate(new Processors(conn));
        }

        private void Btn_Videocards_Click(object sender, RoutedEventArgs e)
        {
            Manager.CurrentPageName = "Видеокарты";
            //CatalogItemFrame.Navigate(new Processors(conn));
        }

        private void Btn_DataStorage_Click(object sender, RoutedEventArgs e)
        {
            Manager.CurrentPageName = "Накопители данных";
           //CatalogItemFrame.Navigate(new Processors(conn));
        }

        private void Btn_AddProduct_Click(object sender, RoutedEventArgs e)
        {
            switch(Manager.CurrentPageName)
            {
                case "Процессоры":
                    {
                        CatalogItemFrame.Navigate(new SetProductPage(conn, 1));
                        break;
                    }
            }
            
            Manager.CurrentPageName = "Добавление товара";
            
        }

        private void Btn_Cart_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}