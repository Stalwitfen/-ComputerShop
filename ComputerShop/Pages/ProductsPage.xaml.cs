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

            switch (Manager.CurrentPageName)
            {
                case "Процессоры":
                    {
                        query = "SELECT code, model, cores, frequency, frequencyRAM, typeRAM, manufacturer, " +
                                "price, availability, imageurl FROM processors JOIN companies ON manufacturer = name";
                        Product.Char1Name = "Ядер (шт)";
                        Product.Char2Name = "Частота (ГГц)";
                        Product.Char3Name = "Частота памяти (МГц)";
                        Product.Char4Name = "Тип памяти";
                        break;
                    }
                case "Оперативная память":
                    {
                        query = "SELECT code, model, formFactor, type, capacity, frequency, manufacturer, " +
                                "price, availability, imageurl FROM RAM JOIN companies ON manufacturer = name";
                        Product.Char1Name = "Форм-фактор";
                        Product.Char2Name = "Тип";
                        Product.Char3Name = "Объём (Гб)";
                        Product.Char4Name = "Частота (МГц)";
                        break;
                    }
                case "Видеокарты":
                    {
                        query = "SELECT code, model, type, capacity, busWidth, chipManufacturer, manufacturer, " +
                                "price, availability, imageurl FROM videocards JOIN companies ON manufacturer = name";
                        Product.Char1Name = "Тип видеопамяти";
                        Product.Char2Name = "Объём (Гб)";
                        Product.Char3Name = "Разрядность шины";
                        Product.Char4Name = "Производитель видеочипа";
                        break;
                    }
                case "Накопители данных":
                    {
                        query = "SELECT code, model, type, capacity, readSpeed, writeSpeed, manufacturer, " +
                                "price, availability, imageurl FROM dataStorage JOIN companies ON manufacturer = name";
                        Product.Char1Name = "Тип";
                        Product.Char2Name = "Объём (Гб)";
                        Product.Char3Name = "Скорость чтения (Мб/с)";
                        Product.Char4Name = "Скорость записи (Мб/с)";
                        break;
                    }
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

            LView_Products.ItemsSource = productsList;

        }

        private void Tb_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            LView_Products.ItemsSource = productsList.FindAll(p => (p.Model.ToLower().Contains(tb_Search.Text.ToLower())));
        }

        private void Btn_OrderByName_Click(object sender, RoutedEventArgs e)
        {
            productsList = productsList.OrderBy(p => p.Model).ToList();
            LView_Products.ItemsSource = productsList;
        }

        private void Btn_OrderByPrice_Click(object sender, RoutedEventArgs e)
        {
            productsList = productsList.OrderBy(p => p.Price).ToList();
            LView_Products.ItemsSource = productsList;
        }

        private void Btn_OrderByManufacturer_Click(object sender, RoutedEventArgs e)
        {
            productsList = productsList.OrderBy(p => p.Manufacturer).ToList();
            LView_Products.ItemsSource = productsList;
        }

        private void Btn_DelProduct_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < productsList.Count; i++)
            {
                if(i == LView_Products.SelectedIndex)
                {
                    if (MessageBox.Show($"Вы точно хотите удалить {productsList[i].Model} ?", "Удаление товара",
                        MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        int CPC = productsList[i].Code;
                        string productType="";
                        if (CPC >= 1000 && CPC < 2000)
                        {
                            productType = "processors";
                        }
                        else if (CPC >= 2000 && CPC < 3000)
                        {
                            productType = "RAM";
                        }
                        else if (CPC >= 3000 && CPC < 4000)
                        {
                            productType = "videocards";
                        }
                        else if (CPC >= 4000 && CPC < 5000)
                        {
                            productType = "dataStorage";
                        }
                        try
                        {
                            string query = "DELETE FROM " + productType + " WHERE code = " + CPC;
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
            LView_Products.ItemsSource = productsList; 
        }

        private void Btn_EditProduct_Click(object sender, RoutedEventArgs e)
        {
            Manager.CurrentPageName = "Изменение товара";
            int currentCode = productsList[LView_Products.SelectedIndex].Code;
            NavigationService.Navigate(new SetProductPage(conn, currentCode));
        }

        private void Btn_AddProduct_Click(object sender, RoutedEventArgs e)
        {
            Cart.shoppingList.Add(productsList[LView_Products.SelectedIndex]);
        }
    }

}
