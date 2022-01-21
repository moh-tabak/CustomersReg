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
using CustomersReg.Services;
using CustomersReg.Models;

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

        private async void SubmitBtn_ClickAsync(object sender, RoutedEventArgs e)
        {
            //if (!ValidateInput()) return;
            await SubmitNewIssueAsync();
            await UpdateIssuesListAsync();
        }


        private void Border_MouseUp(object sender, RoutedEventArgs e)
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

        private async void ILV_LoadedAsync(object sender, RoutedEventArgs e)
        {
            await UpdateIssuesListAsync();
        }

        private async Task UpdateIssuesListAsync()
        {
            ILV.Items.Clear();
            IIssueDataService dataService = new IssueDataService();
            IsuueViewerService viewerService;
            var issues = await dataService.GetAllIssuesAsync();
            foreach (var i in issues)
            {
                viewerService = new IsuueViewerService(i);
                ILV.Items.Add(viewerService);
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

        private void ValidateInput()
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
            CustomerIdInput.Text = string.Empty;
            SubjectInput.Text = string.Empty;
            DescriptionInput.Text = string.Empty;
        }

    }

}
