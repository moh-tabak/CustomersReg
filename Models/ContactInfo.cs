using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CustomersReg.Models
{
    [Table("Contact_Info")]
    public partial class ContactInfo
    {
        [Key]
        public int Id { get; set; }
        [Column("Id_Customers")]
        public int IdCustomers { get; set; }
        [StringLength(12)]
        public string? Mobile { get; set; }
        [StringLength(12)]
        public string? Phone { get; set; }

        [ForeignKey(nameof(IdCustomers))]
        [InverseProperty(nameof(Customer.ContactInfos))]
        public virtual Customer IdCustomersNavigation { get; set; } = null!;
    }
}
