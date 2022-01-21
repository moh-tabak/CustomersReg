using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CustomersReg.Models
{
    public partial class Issue
    {
        public Issue()
        {
            ChangeDates = new HashSet<ChangeDate>();
        }

        [Key]
        public int Id { get; set; }
        [Column("Id_Customers")]
        public int IdCustomers { get; set; }
        [StringLength(200)]
        public string Subject { get; set; } = null!;
        public string Description { get; set; } = null!;
        [Column("Creation_Date", TypeName = "datetime")]
        public DateTime CreationDate { get; set; }
        [Column("Last_Changed", TypeName = "datetime")]
        public DateTime? LastChanged { get; set; }

        [ForeignKey(nameof(IdCustomers))]
        [InverseProperty(nameof(Customer.Issues))]
        public virtual Customer IdCustomersNavigation { get; set; } = null!;
        [InverseProperty(nameof(ChangeDate.IdIssuesNavigation))]
        public virtual ICollection<ChangeDate> ChangeDates { get; set; }
    }
}
