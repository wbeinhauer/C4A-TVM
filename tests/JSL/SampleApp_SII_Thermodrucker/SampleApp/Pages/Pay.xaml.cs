using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
using FirstFloor.ModernUI.Windows.Navigation;
using Microsoft.PointOfService;

namespace SampleApp.Pages
{
    /// <summary>
    /// Interaction logic for Pay.xaml
    /// </summary>
    public partial class Pay : UserControl, IContent
    {
        public Pay()
        {
            InitializeComponent();
        }

        public void OnFragmentNavigation(FirstFloor.ModernUI.Windows.Navigation.FragmentNavigationEventArgs e)
        {
        }

        public void OnNavigatedFrom(FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs e)
        {
            int foo = 0;
        }

        public void OnNavigatedTo(FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs e)
        {
            if (Session.Instance.Price == 0)
                return;
            Ticket.Text = Session.Instance.Ticket; 
            float price = ((float)Session.Instance.Price) / 100;
            TotalPrice.Text = String.Format("Price: {0:0.00} €", price);
            TotalPaid.Text = String.Format("Total paid: {0:0.00} €", 0);
            Remaining.Text = String.Format("Remaining: {0:0.00} €", price);

            CashChanger cashChanger = Session.Instance.CashChanger;
            try
            {
                cashChanger.DataEvent += OnDataEvent;
                cashChanger.DataEventEnabled = true;
                cashChanger.BeginDeposit();
            }
            catch (Exception)
            {
                MessageBox.Show("Cash Unit out of order");
            }
        }

        private void OnDataEvent(object sender, DataEventArgs dataEventArgs)
        {
            CashChanger cashChanger = Session.Instance.CashChanger;
            if (cashChanger.DepositAmount < Session.Instance.Price)
            {
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    TotalPaid.Text = String.Format("Total paid: {0:0.00} €", ((float)cashChanger.DepositAmount) / 100);
                    Remaining.Text = String.Format("Remaining: {0:0.00} €", ((float)(Session.Instance.Price - cashChanger.DepositAmount)) / 100);
                }));
                cashChanger.DataEventEnabled = true;
                return;
            }
            Session.Instance.DepositAmount = cashChanger.DepositAmount;
            cashChanger.EndDeposit(cashChanger.DepositAmount == Session.Instance.Price ? CashDepositAction.NoChange : CashDepositAction.Change);
            Dispatcher.BeginInvoke(new Action(() =>
            {
                TotalPaid.Text = String.Format("Total paid: {0:0.00} €", ((float)cashChanger.DepositAmount) / 100);
                Remaining.Text = String.Format("Remaining: {0:0.00} €", ((float)(Session.Instance.Price - cashChanger.DepositAmount)) / 100);
                Thread.Sleep(1000);
                DefaultLinkNavigator dln = new DefaultLinkNavigator();
                dln.Navigate(new Uri("/Pages/Print.xaml", UriKind.Relative), this);
            }));
        }

        public void OnNavigatingFrom(FirstFloor.ModernUI.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            CashChanger cashChanger = Session.Instance.CashChanger;
            if (cashChanger.DepositAmount < Session.Instance.Price)
            {
                if (e.Source.OriginalString == "/Pages/Print.xaml")
                {
                    e.Cancel = true;
                    return;
                }
                if (e.Source.OriginalString == "/Pages/Home.xaml")
                {
                    try
                    {
                        if (cashChanger.DepositStatus != CashDepositStatus.End)
                        {
                            cashChanger.EndDeposit(CashDepositAction.Repay);
                            Session.Instance.Ticket = "";
                            Session.Instance.Price = 0;
                            Session.Instance.DepositAmount = 0;
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Cash Unit out of order");
                    }
                }
            }
        }
    }
}
