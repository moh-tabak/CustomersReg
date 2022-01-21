using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CustomersReg.Models
{
    public partial class Address
    {
        [Key]
        public int Id { get; set; }
        [Column("Id_Customers")]
        public int IdCustomers { get; set; }
        [Column("Adress_Line")]
        [StringLength(200)]
        public string AdressLine { get; set; } = null!;
        [Column("Postal_Code")]
        [StringLength(8)]
        public string PostalCode { get; set; } = null!;
        [StringLength(20)]
        public string City { get; set; } = null!;
        [StringLength(20)]
        public string? Country { get; set; }

        [ForeignKey(nameof(IdCustomers))]
        [InverseProperty(nameof(Customer.Addresses))]
        public virtual Customer IdCustomersNavigation { get; set; } = null!;
    }
}
