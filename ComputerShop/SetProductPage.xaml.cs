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

    public partial class SetProductPage : Page
    {

        private MySqlConnection conn;
        private int CPC;   //current product code, to shorter
        private string productType;     //it is also the name of the database table

        public SetProductPage(MySqlConnection conn, int currentProductCode)
        {
            InitializeComponent();

            this.conn = conn;
            
            CPC = currentProductCode; //to shorten

            if (CPC == 1 || (CPC >= 1000 && CPC < 2000))
            {
                productType = "processors";
            }
            else if (CPC == 2 || (CPC >= 2000 && CPC < 3000))
            {
                productType = "RAM";
            }
            else if (CPC == 3 || (CPC >= 3000 && CPC < 4000))
            {
                productType = "videocards";
            }
            else if (CPC == 4 || (CPC >= 4000 && CPC < 5000))
            {
                productType = "dataStorage";
            }

            // set label content

            l_Char1.Content = Product.Char1Name;
            l_Char2.Content = Product.Char2Name;
            l_Char3.Content = Product.Char3Name;
            l_Char4.Content = Product.Char4Name;

            // set text boxes, if this is a change to an existing product

            if (CPC > 4)
            {
                try
                {
                    string query = "SELECT * FROM " + productType + " WHERE code = " + CPC;
                    MySqlCommand command = new MySqlCommand(query, conn);
                    MySqlDataReader reader = command.ExecuteReader();
                    reader.Read();

                    tb_Model.Text = reader[1].ToString();
                    tb_Char1.Text = reader[2].ToString();
                    tb_Char2.Text = reader[3].ToString();
                    tb_Char3.Text = reader[4].ToString();
                    tb_Char4.Text = reader[5].ToString();
                    tb_Manufacturer.Text = reader[6].ToString();
                    tb_Price.Text = reader[7].ToString();
                    cb_Availability.IsChecked = Convert.ToBoolean(reader[8]);

                    reader.Close();
                }

                catch (Exception e)
                {
                    WarningMessage.Show("Ошибка! " + e);
                }
            }
        }

        private void Btn_OK_Click(object sender, RoutedEventArgs e)
        {
            bool succesfullExecution = true;

            string model = tb_Model.Text;
            string char1 = tb_Char1.Text;
            string char2 = tb_Char2.Text.Replace(',', '.');        //to avoid an error in the float conversion
            string char3 = tb_Char3.Text;
            string char4 = tb_Char4.Text;
            string manufacturer = tb_Manufacturer.Text;
            string price = tb_Price.Text;
            int availability = Convert.ToInt32(cb_Availability.IsChecked);

            string query="";
            int newProductCode = 0;
            try
            {
                newProductCode = Convert.ToInt32(new MySqlCommand("SELECT MAX(code) FROM " + productType, conn).ExecuteScalar()) + 1;
            }
            catch
            {
                switch (productType)
                {
                    case "processors":
                        {
                            newProductCode = 1000;
                            break;
                        }
                    case "RAM":
                        {
                            newProductCode = 2000;
                            break;
                        }
                    case "videocards":
                        {
                            newProductCode = 3000;
                            break;
                        }
                    case "dataStorage":
                        {
                            newProductCode = 4000;
                            break;
                        }
                }
            }
            


            if (CPC > 999)
            {
                newProductCode = CPC;

                try
                {
                    query = "DELETE FROM " + productType + " WHERE code = " + newProductCode;
                    MySqlCommand command = new MySqlCommand(query, conn);
                    command.ExecuteScalar();
                }
                catch (Exception err)
                {
                    WarningMessage.Show("Ошибка! " + err);
                    succesfullExecution = false;
                }

            }

            query = "INSERT INTO " + productType + " VALUES (" + (newProductCode) + ", '"
                                                                + model + "', '"
                                                                + char1 + "', '"
                                                                + char2 + "', '"
                                                                + char3 + "', '"
                                                                + char4 + "', '"
                                                                + manufacturer + "', "
                                                                + price + ", "
                                                                + availability + ")";

            try
            {
                MySqlCommand command = new MySqlCommand(query, conn);
                command.ExecuteScalar();
            }
            catch (Exception err)
            {
                WarningMessage.Show("Неверно введены данные!");
                //WarningMessage.Show(err.ToString());
                succesfullExecution = false;
            }

            if (succesfullExecution == true)
            {
                switch (productType)
                {
                    case "processors":
                        {
                            Manager.CurrentPageName = "Процессоры";
                            break;
                        }
                    case "RAM":
                        {
                            Manager.CurrentPageName = "Оперативная память";
                            break;
                        }
                    case "videocards":
                        {
                            Manager.CurrentPageName = "Видеокарты";
                            break;
                        }
                    case "dataStorage":
                        {
                            Manager.CurrentPageName = "Накопители данных";
                            break;
                        }
                }
                NavigationService.Navigate(new ProductsPage(conn));
            }
            
                
                
            
        }
    }
}
