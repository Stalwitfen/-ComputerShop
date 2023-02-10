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
    public partial class Processors : Page
    {
        List<Processor> processorsList = new List<Processor>();

        private MySqlConnection conn;
        public Processors(MySqlConnection conn)
        {
            InitializeComponent();
            this.conn = conn;

            string query = "select model, cores, frequency, frequencyRAM, typeRAM, manufacturer, price, imageurl from processors join companies on manufacturer = name";
            MySqlCommand command = new MySqlCommand(query, conn);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                processorsList.Add(new Processor(reader[0].ToString(),
                                                Convert.ToInt32(reader[1]),
                                                Convert.ToDouble(reader[2]),
                                                Convert.ToInt32(reader[3]),
                                                reader[4].ToString(),
                                                reader[5].ToString(),
                                                Convert.ToInt32(reader[6]),
                                                reader[7].ToString() ));
            }
            reader.Close();

            LViewProcesors.ItemsSource = processorsList;

        }
    }

    class Processor
    {
        public Processor(string Model, int Cores, double Frequency, int FrequencyRAM, string TypeRAM, string Manufacturer, int Price, string ImageURL)
        {
            this.Model = Model;
            this.Cores = Cores;
            this.Frequency = Frequency;
            this.FrequencyRAM = FrequencyRAM;
            this.TypeRAM = TypeRAM;
            this.Manufacturer = Manufacturer;
            this.Price = Price;
            this.ImageURL = ImageURL;
        }

        public string Model { get; set; }
        public int Cores { get; set; }
        public double Frequency { get; set; }
        public int FrequencyRAM { get; set; }
        public string TypeRAM { get; set; }
        public string Manufacturer { get; set; }
        public int Price { get; set; }
        public string ImageURL { get; set; }

    }
}
