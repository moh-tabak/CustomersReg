using CustomersReg.Models;
using CustomersReg.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomersReg.ViewModels
{
    internal class Customers_ViewModel
    {
        public int? CustomerId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? AdressLine { get; }
        public string? PostalCode { get; }
        public string? City { get; }
        public string? Country { get; }
        public string? Mobile { get; }
        public string? Phone { get; }

        public Customers_ViewModel()
        {
        }
        public Customers_ViewModel(Customer customer)
        {
            ICustomerDataService dataService = new CustomerDataService();

            CustomerId = customer.Id;
            Name = customer.Name;
            Email = customer.Email;
            Address? address = dataService.GetAdress(customer.Id);
            if (address == null)
            {
                AdressLine = null;
                PostalCode = null;
                City = null;
                Country = null;
            }
            else
            {
                AdressLine = address.AdressLine;
                PostalCode = address.PostalCode;
                City = address.City;
                Country = address.Country;
            }
            ContactInfo? contactInfo = dataService.GetContactInfo(customer.Id);
            if (contactInfo == null)
            {
                Phone = null;
                Mobile = null;
            }
            else
            {
                Phone = contactInfo.Phone;
                Mobile = contactInfo.Mobile;
            }

        }
    }
}

