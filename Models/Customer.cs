using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CustomersReg.Models
{
    [Index(nameof(Email), Name = "UQ__Customer__A9D10534B084F744", IsUnique = true)]
    public partial class Customer
    {
        public Customer()
        {
            Addresses = new HashSet<Address>();
            ContactInfos = new HashSet<ContactInfo>();
            Issues = new HashSet<Issue>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; } = null!;
        [StringLength(100)]
        public string Email { get; set; } = null!;

        [InverseProperty(nameof(Address.IdCustomersNavigation))]
        public virtual ICollection<Address> Addresses { get; set; }
        [InverseProperty(nameof(ContactInfo.IdCustomersNavigation))]
        public virtual ICollection<ContactInfo> ContactInfos { get; set; }
        [InverseProperty(nameof(Issue.IdCustomersNavigation))]
        public virtual ICollection<Issue> Issues { get; set; }
    }
}
