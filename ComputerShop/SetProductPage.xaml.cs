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
        private int lastProductCode;
        private string productType;
        public SetProductPage(MySqlConnection conn, int? currentProductCode)
        {
            InitializeComponent();
            switch (currentProductCode)
            {
                case 1:
                    {
                        productType = "processors";
                        break;
                    }
                case 2:
                    {
                        productType = "RAM";
                        break;
                    }
                case 3:
                    {
                        productType = "videocards";
                        break;
                    }
                case 4:
                    {
                        productType = "dataStorage";
                        break;
                    }
            }

        }
    }
}
