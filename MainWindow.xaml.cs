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
using CustomersReg.Views;
using CustomersReg.ViewModels;
using CustomersReg.Models;

namespace CustomersReg
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MnuBtn_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            ResetMenuBtns();
            button.Background = new SolidColorBrush(Color.FromRgb(190, 230, 253));
            button.Foreground = new SolidColorBrush(Color.FromRgb(192, 126, 0));
            switch (button.Name)
            {
                case "MnuBtnCustomers":
                    MainViewer.DataContext = new Customers_ViewModel();
                    break;
                case "MnuBtnIssues":
                    MainViewer.DataContext = new Issues_ViewModel();
                    break;
            }
        }

        private void ResetMenuBtns()
        {
            foreach (Button b in SideMenu.Children)
            {
                b.Background = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));
                b.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            }
        }

        private void Customers_View_NewIssue(object sender, RoutedEventArgs e)
        {
            if (Customers_View.SelectedCustomerID == 0) return;
            Issues_ViewModel iview = new Issues_ViewModel(Customers_View.SelectedCustomerID);
            MainViewer.DataContext = iview;
            ResetMenuBtns();
            MnuBtnIssues.Background = new SolidColorBrush(Color.FromRgb(190, 230, 253));
            MnuBtnIssues.Foreground = new SolidColorBrush(Color.FromRgb(192, 126, 0));
        }
    }


    enum SecondaryViewState
    {
        Collapsed,
        Expanded
    }
}
