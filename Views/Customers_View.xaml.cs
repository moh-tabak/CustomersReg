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
using CustomersReg.Models;
using CustomersReg.Services;

namespace CustomersReg.Views
{
    /// <summary>
    /// Interaction logic for Customers_View.xaml
    /// </summary>
    public partial class Customers_View : UserControl
    {

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
        private void BtnCopyId_Click(object sender, RoutedEventArgs e)
        {
            var btn = (Button)sender;
            var s = (CustomerViewerService)btn.DataContext;
            Clipboard.SetText(Convert.ToString(s.CustomerId));
        }
        private async void SubmitBtn_ClickAsync(object sender, RoutedEventArgs e)
        {
            //if (!ValidateInput()) return;
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
            CustomerViewerService viewerService;
            var customers = await dataservice.GetAllCustomersAsync();
            foreach (Customer c in customers)
            {
                viewerService = new CustomerViewerService(c);
                _ = CLV.Items.Add(viewerService);
            }
        }
        private async Task SubmitNewCustomerAsync()
        {
            ICustomerDataService service = new CustomerDataService();

            try
            {
                await service.AddCustomerAsync(new Customer()
                {
                    Name = FirstNameInput.Text + LastNameInput.Text,
                    Email = EmailInput.Text,
                    Addresses = new List<Address>()
                    {
                        new Address()
                        {
                            AdressLine=AddressLineInput.Text,
                            PostalCode=PostalCodeInput.Text,
                            City=CityInput.Text,
                            Country=CountryInput.Text
                        }
                    },
                    ContactInfos = new List<ContactInfo>()
                    {
                        new ContactInfo()
                        {
                            Phone=PhoneInput.Text,
                            Mobile=MobileInput.Text,
                        }
                    }
                });
            }
            catch { MessageBox.Show("Error"); return; }
            SecondaryView_Clear();
        }

        private bool ValidateInput()
        {
            throw new NotImplementedException();
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
