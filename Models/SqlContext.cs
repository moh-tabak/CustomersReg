using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CustomersReg.Models
{
    public partial class SqlContext : DbContext
    {
        public SqlContext()
        {
        }

        public SqlContext(DbContextOptions<SqlContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Addresses { get; set; } = null!;
        public virtual DbSet<ChangeDate> ChangeDates { get; set; } = null!;
        public virtual DbSet<ContactInfo> ContactInfos { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Issue> Issues { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\moh\\Desktop\\DataLag\\Inlämning\\CustomersReg\\Data\\DataBaseFile_Sql.mdf;Integrated Security=True;Connect Timeout=30");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.HasOne(d => d.IdCustomersNavigation)
                    .WithMany(p => p.Addresses)
                    .HasForeignKey(d => d.IdCustomers)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Addresses__Id_Cu__52593CB8");
            });

            modelBuilder.Entity<ChangeDate>(entity =>
            {
                entity.HasOne(d => d.IdIssuesNavigation)
                    .WithMany(p => p.ChangeDates)
                    .HasForeignKey(d => d.IdIssues)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ChangeDat__Id_Is__4F7CD00D");
            });

            modelBuilder.Entity<ContactInfo>(entity =>
            {
                entity.HasOne(d => d.IdCustomersNavigation)
                    .WithMany(p => p.ContactInfos)
                    .HasForeignKey(d => d.IdCustomers)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Contact_I__Id_Cu__5165187F");
            });

            modelBuilder.Entity<Issue>(entity =>
            {
                entity.HasOne(d => d.IdCustomersNavigation)
                    .WithMany(p => p.Issues)
                    .HasForeignKey(d => d.IdCustomers)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Issues__Id_Custo__5070F446");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
