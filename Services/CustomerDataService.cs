using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomersReg.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomersReg.Services
{
    internal interface ICustomerDataService
    {
        Customer GetCustomerById(int Id);
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task AddCustomerAsync(Customer customer);
        Address GetAdress(int CustomerId);
        ContactInfo GetContactInfo(int CustomerId);
    }

    internal class CustomerDataService : ICustomerDataService
    {
        public async Task AddCustomerAsync(Customer customer)
        {
            using (var cxt = new SqlContext())
            {
                await cxt.AddAsync(customer);
                await cxt.SaveChangesAsync();
            }

        }

        public Address GetAdress(int CustomerId)
        {
            Address? a;
            using (var cxt = new SqlContext())
            {
                a = cxt.Addresses.Single(f => f.IdCustomers == CustomerId);
            }
            return a;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            var c = new List<Customer>();
            using (var cxt = new SqlContext())
            {
                c = await cxt.Customers.ToListAsync();
            }
            return c;
        }

        public ContactInfo GetContactInfo(int CustomerId)
        {
            ContactInfo? ci;
            using (var cxt = new SqlContext())
            {
                ci = cxt.ContactInfos.Single(f => f.IdCustomers == CustomerId);
            }
            return ci;
        }

        public Customer GetCustomerById(int Id)
        {
            Customer c;
            using (var cxt = new SqlContext())
            {
                c = cxt.Customers.Single(f => f.Id == Id);
            }
            return c;
        }
    }
}
