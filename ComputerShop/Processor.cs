using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ComputerShop
{
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
