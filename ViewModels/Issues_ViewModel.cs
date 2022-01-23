using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomersReg.Models;
using CustomersReg.Services;
using CustomersReg.Views;
using System.ComponentModel;

namespace CustomersReg.ViewModels
{
    internal class Issues_ViewModel
    {
        public int CustomerId { get; private set; }
        public string? CustomerName { get; private set; }
        public DateTime CreationDate { get; private set; }
        public DateTime? LastChanged { get; private set; }
        public string? Subject { get; private set; }
        public string? Description { get; private set; }
        public string OpenSecodaryOnLoading { get; private set; } = "No";

        public Issues_ViewModel()
        {

        }

        public Issues_ViewModel(Issue issue)
        {
            ICustomerDataService customerDataService = new CustomerDataService();
            Customer _customer = customerDataService.GetCustomerById(issue.IdCustomers);
            Subject = issue.Subject;
            Description = issue.Description;
            CustomerId = issue.IdCustomers;
            CustomerName = _customer.Name;
            CreationDate = issue.CreationDate;
            LastChanged = issue.LastChanged;
        }
        public Issues_ViewModel(int customerId)
        {
            CustomerId = customerId;
            OpenSecodaryOnLoading = "Yes";
        }

    }

}

