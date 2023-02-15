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

    public partial class ProductsPage : Page
    {
        List<Product> productsList = new List<Product>();

        private MySqlConnection conn;
        public ProductsPage(MySqlConnection conn)
        {
            InitializeComponent();
            this.conn = conn;

            string query="";

            if (Manager.CurrentPageName == "Процессоры")
            {
                query = "select code, model, cores, frequency, frequencyRAM, typeRAM, manufacturer, " +
                    "price, availability, imageurl from processors join companies on manufacturer = name";
            }
            
            try
            {
                
                MySqlCommand command = new MySqlCommand(query, conn);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    productsList.Add(new Product(Convert.ToInt32(reader[0]),
                                                    reader[1].ToString(),
                                                    reader[2].ToString(),
                                                    reader[3].ToString(),
                                                    reader[4].ToString(),
                                                    reader[5].ToString(),
                                                    reader[6].ToString(),
                                                    Convert.ToInt32(reader[7]),
                                                    Convert.ToBoolean(reader[8]),
                                                    reader[9].ToString()));
                }
                reader.Close();
            }

            catch (Exception e)
            {
                WarningMessage.Show("Ошибка! " + e);
            }

            LViewProducts.ItemsSource = productsList;

        }

        private void Tb_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            LViewProducts.ItemsSource = productsList.FindAll(p => (p.Model.ToLower().Contains(tb_Search.Text.ToLower())));
        }

        private void Btn_OrderByName_Click(object sender, RoutedEventArgs e)
        {
            productsList = productsList.OrderBy(p => p.Model).ToList();
            LViewProducts.ItemsSource = productsList;
        }

        private void Btn_OrderByPrice_Click(object sender, RoutedEventArgs e)
        {
            productsList = productsList.OrderBy(p => p.Price).ToList();
            LViewProducts.ItemsSource = productsList;
        }

        private void Btn_OrderByManufacturer_Click(object sender, RoutedEventArgs e)
        {
            productsList = productsList.OrderBy(p => p.Manufacturer).ToList();
            LViewProducts.ItemsSource = productsList;
        }

        private void Btn_DelProduct_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < productsList.Count; i++)
            {
                if(i == LViewProducts.SelectedIndex)
                {
                    if (MessageBox.Show($"Вы точно хотите удалить {productsList[i].Model} ?", "Удаление товара",
                        MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        try
                        {
                            string query = "DELETE FROM processors WHERE code = " + productsList[i].Code;
                            MySqlCommand command = new MySqlCommand(query, conn);
                            command.ExecuteScalar();
                        }

                        catch (Exception err)
                        {
                            WarningMessage.Show("Ошибка! " + err);
                        }


                        productsList.RemoveAt(i);
                    }    
                    break;
                }
            }
            productsList = productsList.OrderBy(p => p.Code).ToList();
            LViewProducts.ItemsSource = productsList; 
        }

        private void Btn_EditProduct_Click(object sender, RoutedEventArgs e)
        {
            Manager.CurrentPageName = "Изменение товара";
            int currentCode = productsList[LViewProducts.SelectedIndex].Code;
            NavigationService.Navigate(new SetProductPage(conn, currentCode));
        }
    }

}
