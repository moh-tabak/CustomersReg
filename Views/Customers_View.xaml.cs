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
using System.Text.RegularExpressions;
using CustomersReg.Models;
using CustomersReg.ViewModels;
using CustomersReg.Services;

namespace CustomersReg.Views
{
    /// <summary>
    /// Interaction logic for Customers_View.xaml
    /// </summary>
    public partial class Customers_View : UserControl
    {
        public static readonly RoutedEvent ButtonNewIssueEvent = EventManager.RegisterRoutedEvent("BtnNewIssue_clicked", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Customers_View));

        public event RoutedEventHandler NewIssue_Clicked
        {
            add { AddHandler(ButtonNewIssueEvent, value); }
            remove { RemoveHandler(ButtonNewIssueEvent, value); }
        }
        public static int SelectedCustomerID { get; set; }

        private SecondaryViewState currentSecondaryView = SecondaryViewState.Collapsed;
        public Customers_View()
        {
            InitializeComponent();
        }


        private void Border_MouseUp(object sender, MouseButtonEventArgs e)
        {
            switch (currentSecondaryView)
            {
                case SecondaryViewState.Collapsed:
                    SecondaryView_Expand();
                    break;
                case SecondaryViewState.Expanded:
                    SecondaryView_Collapse();
                    break;
            }
        }

        private async void SubmitBtn_ClickAsync(object sender, RoutedEventArgs e)
        {
            if (!ValidateInput()) return;
            await SubmitNewCustomerAsync();
            await UpdateCustomersListAsync();
        }
        private async void CLV_LoadedAsync(object sender, RoutedEventArgs e)
        {
            await UpdateCustomersListAsync();
        }

        private async Task UpdateCustomersListAsync()
        {
            CLV.Items.Clear();
            ICustomerDataService dataservice = new CustomerDataService();
            Customers_ViewModel viewerService;
            var customers = await dataservice.GetAllCustomersAsync();
            foreach (Customer c in customers)
            {
                viewerService = new Customers_ViewModel(c);
                _ = CLV.Items.Add(viewerService);
            }
        }
        private void BtnNewIssue_Click(object sender, RoutedEventArgs e)
        {
            var btn = (Button)sender;
            var data = (Customers_ViewModel)btn.DataContext;
            if (data.CustomerId != null)
            {
                SelectedCustomerID = (int)data.CustomerId;
                RaiseEvent(new RoutedEventArgs(ButtonNewIssueEvent));
            }
        }

        private async Task SubmitNewCustomerAsync()
        {
            ICustomerDataService service = new CustomerDataService();

            try
            {
                await service.AddCustomerAsync(new Customer()
                {
                    Name = $"{FirstNameInput.Text.Trim()} {LastNameInput.Text.Trim()}",
                    Email = EmailInput.Text.Trim(),
                    Addresses = new List<Address>()
                    {
                        new Address()
                        {
                            AdressLine=AddressLineInput.Text.Trim(),
                            PostalCode=PostalCodeInput.Text.Trim(),
                            City=CityInput.Text.Trim(),
                            Country=CountryInput.Text.Trim()
                        }
                    },
                    ContactInfos = new List<ContactInfo>()
                    {
                        new ContactInfo()
                        {
                            Phone=PhoneInput.Text.Trim(),
                            Mobile=MobileInput.Text.Trim(),
                        }
                    }
                });
            }
            catch { MessageBox.Show("Error"); return; }
            SecondaryView_Clear();
        }

        private bool ValidateInput()
        {
            string? msg = null;
            if (FirstNameInput.Text.Length < 2)
            {
                msg = "*Förnamn ";
            }
            if (LastNameInput.Text.Length < 2)
            {
                msg += "*Efternamn ";
            }
            if (EmailInput.Text.Length < 6 || !EmailInput.Text.Contains('@') || !EmailInput.Text.Contains('.'))
            {
                msg += "*E-postadress ";
            }
            if (PhoneInput.Text != "")
            {
                if (!Regex.IsMatch(PhoneInput.Text, "^.{6,15}") || !Regex.IsMatch(PhoneInput.Text, "^[0+]") || Regex.IsMatch(PhoneInput.Text, @"^.*[A-Za-z_.%&',;=!¤€%'£@""/\\\$?].*$"))
                {
                    msg += "*Telefonnummer ";
                }
            }
            if (MobileInput.Text == "" || !Regex.IsMatch(MobileInput.Text, "^.{6,15}") || !Regex.IsMatch(MobileInput.Text, "^[0+]") || Regex.IsMatch(MobileInput.Text, @"^.*[A-Za-z_.%&',;=!¤€%'£@""/\\\$?].*$"))
            {
                msg += "*Mobilonnummer ";
            }
            if (AddressLineInput.Text.Length < 4)
            {
                msg += "*Adress ";
            }
            if (PostalCodeInput.Text == "" || !Regex.IsMatch(PostalCodeInput.Text, "^.{5,8}") || Regex.IsMatch(PostalCodeInput.Text, @"^.*[A-Za-z_.%&',;=!+¤€%'£@""/\\\$?].*$"))
            {
                msg += "*Postnummer ";
            }
            if (CityInput.Text.Length < 2)
            {
                msg += "*Ort ";
            }
            if (msg != null)
            {
                msg = "Dessa fält saknas, eller är ogiltiga: " + msg;
                ErrorMsgTxt.Text = msg;
                ErrorMsgTxt.Visibility = Visibility.Visible;
                return false;
            }
            else
            {
                ErrorMsgTxt.Visibility = Visibility.Hidden;
                return true;
            }
        }

        private void SecondaryView_Expand()
        {
            var txt = (TextBlock)BtnSecondaryView.Child;
            txt.Text = ">";
            SecondaryView.Width = 420;
            currentSecondaryView = SecondaryViewState.Expanded;
        }

        private void SecondaryView_Collapse()
        {
            var txt = (TextBlock)BtnSecondaryView.Child;
            txt.Text = "+";
            SecondaryView.Width = 0;
            currentSecondaryView = SecondaryViewState.Collapsed;
            SecondaryView_Clear();
        }

        private void SecondaryView_Clear()
        {
            FirstNameInput.Text = string.Empty;
            LastNameInput.Text = string.Empty;
            EmailInput.Text = string.Empty;
            PhoneInput.Text = string.Empty;
            MobileInput.Text = string.Empty;
            AddressLineInput.Text = string.Empty;
            PostalCodeInput.Text = string.Empty;
            CityInput.Text = string.Empty;
            CountryInput.Text = string.Empty;
        }



    }
}
