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
using FirstFloor.ModernUI.Windows;

namespace SampleApp.Pages
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : UserControl, IContent
    {
        public Home()
        {
            InitializeComponent();
        }

        private void WholeDayTicketButton_Click(object sender, RoutedEventArgs e)
        {
            Session.Instance.Price = 850;
            Session.Instance.Ticket = "Whole Day Ticket";
        }

        private void TenTicketsButton_Click(object sender, RoutedEventArgs e)
        {
            Session.Instance.Price = 610;
            Session.Instance.Ticket = "Ten Tickets";
        }

        private void FiveTicketsButton_Click(object sender, RoutedEventArgs e)
        {
            Session.Instance.Price = 350;
            Session.Instance.Ticket = "Five Tickets";
        }

        private void SingleTicketButton_Click(object sender, RoutedEventArgs e)
        {
            Session.Instance.Price = 200;
            Session.Instance.Ticket = "Single Ticket";
        }

        public void OnFragmentNavigation(FirstFloor.ModernUI.Windows.Navigation.FragmentNavigationEventArgs e)
        {
        }

        public void OnNavigatedFrom(FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs e)
        {
        }

        public void OnNavigatedTo(FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs e)
        {
        }

        public void OnNavigatingFrom(FirstFloor.ModernUI.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            if (Session.Instance.Price == 0)
                e.Cancel = true;
        }
    }
}
