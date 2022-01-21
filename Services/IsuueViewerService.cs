using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomersReg.Models;

namespace CustomersReg.Services
{
    internal class IsuueViewerService : Issue
    {
        public int CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public IsuueViewerService()
        {
        }
        public IsuueViewerService(Issue issue)
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
    }
}
