using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ComputerShop
{
    class Product
    {
        public Product (int Code, string Model, string Char1Value, string Char2Value, string Char3Value, 
                        string Char4Value,string Manufacturer, int Price, bool Availability, string ImageURL)
        {
            this.Code = Code;
            this.Model = Model;
            this.Char1Value = Char1Value;
            this.Char2Value = Char2Value;
            this.Char3Value = Char3Value;
            this.Char4Value = Char4Value;
            this.Manufacturer = Manufacturer;
            this.Price = Price;
            this.Availability = Availability;
            this.ImageURL = ImageURL;
        }

        public int Code { get; set; }
        public string Model { get; set; }
        public string Char1Value { get; set; }
        public string Char2Value { get; set; }
        public string Char3Value { get; set; }
        public string Char4Value { get; set; }
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

        
        public string Char1
        {
            get
            {
                return Char1Name + ": " + Char1Value;
            }
        }
        public string Char2
        {
            get
            {
                return Char1Name + ": " + Char1Value;
            }
        }
        public string Char3
        {
            get
            {
                return Char1Name + ": " + Char1Value;
            }
        }
        public string Char4
        {
            get
            {
                return Char1Name + ": " + Char1Value;
            }
        }
        public string Char1Name { get; set; }
        public string Char2Name { get; set; }
        public string Char3Name { get; set; }
        public string Char4Name { get; set; }

        private bool availability;
    }
}
