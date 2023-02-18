using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ComputerShop
{
    class Manager
    {
        public static Frame MainFrame { get; set; }
        public static string Access
        {
            get
            {
                return access;
            }
            set
            {
                access = value;
                if (access == "admin")
                {
                    AdminButtonsVisibility = System.Windows.Visibility.Visible;
                }
                else
                {
                    AdminButtonsVisibility = System.Windows.Visibility.Hidden;
                }
            }
        }
        public static string CurrentPageName { get; set; }
        public static System.Windows.Visibility AdminButtonsVisibility { get; set; }

        private static string access;
    }
}

