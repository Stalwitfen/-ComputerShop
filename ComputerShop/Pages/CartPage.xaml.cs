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
    public partial class CartPage : Page
    {
        int finalPrice = 0;
        public CartPage()
        {
            InitializeComponent();
            LView_ShoppingList.ItemsSource = Cart.shoppingList;
            
            foreach(Product product in Cart.shoppingList)
            {
                finalPrice += product.Price;
            }
            l_FinalPrice.Content = finalPrice;
        }

        private void Btn_DelProductFromCart_Click(object sender, RoutedEventArgs e)
        {
            Cart.shoppingList.RemoveAt(LView_ShoppingList.SelectedIndex);
            NavigationService.Navigate(new CartPage());
        }

        private void Btn_ClearTheCart_Click(object sender, RoutedEventArgs e)
        {
            Cart.shoppingList.Clear();
            NavigationService.Navigate(new CartPage());
        }
    }
}
