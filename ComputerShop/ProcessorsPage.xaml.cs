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
    /// <summary>
    /// Логика взаимодействия для Processors.xaml
    /// </summary>
    public partial class ProcessorsPage : Page
    {
        List<Processor> processorsList = new List<Processor>();

        private MySqlConnection conn;
        public ProcessorsPage(MySqlConnection conn)
        {
            InitializeComponent();
            this.conn = conn;

            string query = "select code, model, cores, frequency, frequencyRAM, typeRAM, manufacturer, price, availability, imageurl from processors join companies on manufacturer = name";
            MySqlCommand command = new MySqlCommand(query, conn);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                processorsList.Add(new Processor(Convert.ToInt32(reader[0]),
                                                reader[1].ToString(),
                                                Convert.ToInt32(reader[2]),
                                                Convert.ToDouble(reader[3]),
                                                Convert.ToInt32(reader[4]),
                                                reader[5].ToString(),
                                                reader[6].ToString(),
                                                Convert.ToInt32(reader[7]),
                                                Convert.ToBoolean(reader[8]),
                                                reader[9].ToString() ));
            }
            reader.Close();

            LViewProcesors.ItemsSource = processorsList;

        }

        private void Tb_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            LViewProcesors.ItemsSource = processorsList.FindAll(p => (p.Model.ToLower().Contains(tb_Search.Text.ToLower())));
        }

        private void Btn_OrderByName_Click(object sender, RoutedEventArgs e)
        {
            processorsList = processorsList.OrderBy(p => p.Model).ToList();
            LViewProcesors.ItemsSource = processorsList;
        }

        private void Btn_OrderByPrice_Click(object sender, RoutedEventArgs e)
        {
            processorsList = processorsList.OrderBy(p => p.Price).ToList();
            LViewProcesors.ItemsSource = processorsList;
        }

        private void Btn_OrderByCores_Click(object sender, RoutedEventArgs e)
        {
            processorsList = processorsList.OrderBy(p => p.Cores).ToList();
            LViewProcesors.ItemsSource = processorsList;
        }

        private void Btn_OrderByFrequency_Click(object sender, RoutedEventArgs e)
        {
            processorsList = processorsList.OrderBy(p => p.Frequency).ToList();
            LViewProcesors.ItemsSource = processorsList;
        }

        private void Btn_OrderByFrequencyRAM_Click(object sender, RoutedEventArgs e)
        {
            processorsList = processorsList.OrderBy(p => p.FrequencyRAM).ToList();
            LViewProcesors.ItemsSource = processorsList;
        }

        private void Btn_OrderByTypeRAM_Click(object sender, RoutedEventArgs e)
        {
            processorsList = processorsList.OrderBy(p => p.TypeRAM).ToList();
            LViewProcesors.ItemsSource = processorsList;
        }

        private void Btn_OrderByManufacturer_Click(object sender, RoutedEventArgs e)
        {
            processorsList = processorsList.OrderBy(p => p.Manufacturer).ToList();
            LViewProcesors.ItemsSource = processorsList;
        }

        private void Btn_DelProduct_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < processorsList.Count; i++)
            {
                if(i == LViewProcesors.SelectedIndex)
                {
                    if (MessageBox.Show($"Вы точно хотите удалить {processorsList[i].Model} ?", "Удаление товара",
                        MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        string query = "DELETE FROM processors WHERE code = " + processorsList[i].Code;
                        MySqlCommand command = new MySqlCommand(query, conn);
                        command.ExecuteScalar();

                        processorsList.RemoveAt(i);
                    }    
                    break;
                }
            }
            processorsList = processorsList.OrderBy(p => p.Code).ToList();
            LViewProcesors.ItemsSource = processorsList; 
        }

        private void Btn_EditProduct_Click(object sender, RoutedEventArgs e)
        {

        }
    }

    class Processor
    {
        public Processor(int Code, string Model, int Cores, double Frequency, int FrequencyRAM, string TypeRAM, string Manufacturer, int Price, bool Availability, string ImageURL)
        {
            this.Code = Code;
            this.Model = Model;
            this.Cores = Cores;
            this.Frequency = Frequency;
            this.FrequencyRAM = FrequencyRAM;
            this.TypeRAM = TypeRAM;
            this.Manufacturer = Manufacturer;
            this.Price = Price;
            this.Availability = Availability;
            this.ImageURL = ImageURL;
        }

        public int Code { get; set; }
        public string Model { get; set; }
        public int Cores { get; set; }
        public double Frequency { get; set; }
        public int FrequencyRAM { get; set; }
        public string TypeRAM { get; set; }
        public string Manufacturer { get; set; }
        public int Price { get; set; }
        public bool Availability
        {
            get
            {
                return availability;
            }
            set
            {
                if (value == false)
                {
                    Visibility = Visibility.Hidden;
                    ImageFilter = "Gray32Float";
                }
                else
                {
                    Visibility = Visibility.Visible;
                    ImageFilter = "Pbgra32";
                }
                availability = value;
            }
        }
        public string ImageURL { get; set; }
        public Visibility Visibility { get; set; }
        public string ImageFilter { get; set; }

        private bool availability;
    }
}
