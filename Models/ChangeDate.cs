using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CustomersReg.Models
{
    public partial class ChangeDate
    {
        [Key]
        public int Id { get; set; }
        [Column("Id_Issues")]
        public int IdIssues { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOrChanged { get; set; }

        [ForeignKey(nameof(IdIssues))]
        [InverseProperty(nameof(Issue.ChangeDates))]
        public virtual Issue IdIssuesNavigation { get; set; } = null!;
    }
}
