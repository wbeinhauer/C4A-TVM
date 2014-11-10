using FirstFloor.ModernUI.Windows.Controls;
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
using System.ComponentModel;
using Microsoft.PointOfService;

namespace SampleApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ModernWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (Session.Instance.PosPrinter != null)
                Session.Instance.PosPrinter.Close();
            if (Session.Instance.CashChanger != null)
            {
                CashCounts cashCounts = Session.Instance.CashChanger.ReadCashCounts();
                Session.Instance.CashChanger.DispenseCash(cashCounts.Counts);
                Session.Instance.CashChanger.Close();
            }
        }
    }
}
