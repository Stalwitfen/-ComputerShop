using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ComputerShop
{
    class WarningMessage
    {
        public static void Show(string value)
        {
            MessageBox.Show(value, "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }

}
