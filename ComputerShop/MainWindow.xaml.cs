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
using System.Windows.Threading;
using MySql.Data.MySqlClient;
using SqlConn;

namespace ComputerShop
{
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();
            Manager.CurrentPageName = "Авторизация";
            MainFrame.Navigate(new Authentification());
            btn_Exit.Visibility = Visibility.Hidden;

            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            timer.Start();
        }

        private void Btn_Exit_Click(object sender, RoutedEventArgs e)
        {
            Manager.CurrentPageName = "Авторизация";
            MainFrame.Navigate(new Authentification());
            btn_Exit.Visibility = Visibility.Hidden;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            l_CurrentPage.Content = Manager.CurrentPageName;
            if(Manager.CurrentPageName != "Авторизация")
                btn_Exit.Visibility = Visibility.Visible;
        }
    }
}
