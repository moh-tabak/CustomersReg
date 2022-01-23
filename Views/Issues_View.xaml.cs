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
using CustomersReg.Services;
using CustomersReg.Models;
using CustomersReg.ViewModels;

namespace CustomersReg.Views
{
    /// <summary>
    /// Interaction logic for Issues_View.xaml
    /// </summary>
    public partial class Issues_View : UserControl
    {
        private SecondaryViewState currentSecondaryView = SecondaryViewState.Collapsed;

        public Issues_View()
        {
            InitializeComponent();
        }

        public Issues_View(int CustomerId)
        {
            InitializeComponent();
            SecondaryView_Expand();
            CustomerIdInput.Text = Convert.ToString(CustomerId);
        }

        private async void SubmitBtn_ClickAsync(object sender, RoutedEventArgs e)
        {
            if (!ValidateInput()) return;
            await SubmitNewIssueAsync();
            await UpdateIssuesListAsync();
        }


        private void Border_MouseUp(object sender, RoutedEventArgs e)
        {
            //Plus/pil Knappen blev tryckt. Kan göras som Command istället.
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

        private async void ILV_LoadedAsync(object sender, RoutedEventArgs e)
        {
            //Om denna vyn öppnades med "Nytt Ärende" knappen, visa SecondaryView.
            if (TempTxt.Text == "Yes")
            {
                SecondaryView_Expand();
                CustomerIdInput.Text = Convert.ToString(Customers_View.SelectedCustomerID);
                Customers_View.SelectedCustomerID = 0;
                SubjectInput.Focus();
            }
            await UpdateIssuesListAsync();
        }

        private async Task UpdateIssuesListAsync()
        {
            ILV.Items.Clear();
            IIssueDataService dataService = new IssueDataService();
            Issues_ViewModel viewModel;
            var issues = await dataService.GetAllIssuesAsync();
            foreach (var i in issues)
            {
                viewModel = new Issues_ViewModel(i);
                ILV.Items.Add(viewModel);
            }
        }

        private async Task SubmitNewIssueAsync()
        {
            IIssueDataService dataService = new IssueDataService();
            try
            {
                await dataService.AddIssueAsync(new Issue()
                {
                    IdCustomers = int.Parse(CustomerIdInput.Text),
                    Subject = SubjectInput.Text,
                    Description = DescriptionInput.Text,
                    CreationDate = DateTime.Now
                }); ;
            }
            catch { MessageBox.Show("Error"); return; }
            SecondaryView_Clear();
        }

        private bool ValidateInput()
        {
            string? msg = null;
            if (CustomerIdInput.Text == "" || !Regex.IsMatch(CustomerIdInput.Text.Trim(), "^[0-9]*$") || int.Parse(CustomerIdInput.Text) == 0)
            {
                msg = "*Kundnmmer ";
            }
            if (SubjectInput.Text.Length < 2)
            {
                msg += "*Rubrik ";
            }
            if (DescriptionInput.Text.Length < 10)
            {
                msg += "*Beskrivning ";
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
            CustomerIdInput.Text = string.Empty;
            SubjectInput.Text = string.Empty;
            DescriptionInput.Text = string.Empty;
        }

    }

}
