using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using P03_SalesDataBase.Data.Models;

namespace P03_SalesDataBase.Data
{
    public class SalesContext : DbContext
    {
        public SalesContext(DbContextOptions<SalesContext> options) : base(options)
        {

        }
        public DbSet<Customer> customers => Set<Customer>();
        public DbSet<Product> products => Set<Product>();
        public DbSet<Sale> sales => Set<Sale>();
        public DbSet<Store> stores => Set<Store>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.CustomerId).HasName("PK__Customers");
                entity.Property(e => e.Name).HasMaxLength(100);
                entity.Property(e => e.Email).HasMaxLength(80);
                entity.Property(e => e.CreditCardNumber);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.ProductId).HasName("PK__Products__");

                entity.Property(e => e.Name).HasMaxLength(50);
                entity.Property(e => e.Quantity);
                entity.Property(e => e.Price);
                entity.Property(e => e.Description).HasMaxLength(250);
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.HasKey(e => e.StoreId).HasName("PK__Stores__");

                entity.Property(e => e.Name).HasMaxLength(80);
            });

            modelBuilder.Entity<Sale>(entity =>
            {
                entity.HasKey(e => e.SaleId).HasName("PK__Sales");
                entity.Property(e => e.Date)
                    .HasDefaultValueSql("(datetime('now'))")
                    .HasColumnType("datetime");
                entity.HasOne(d => d.Customer).WithMany(p => p.Sales)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Sales_Customers");
                entity.HasOne(d => d.Product).WithMany(p => p.Sales)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Sales_Products");
                entity.HasOne(d => d.Store).WithMany(p => p.Sales)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Sales_Stores");
            });

            /*modelBuilder.Entity<Visitations>(entity =>
            {
                entity.HasKey(e => e.VisitationId).HasName("PK__Visistations__");

                entity.Property(e => e.Comments).HasMaxLength(250);
                entity.Property(e => e.Date)
                    .HasDefaultValueSql("(now())")
                    .HasColumnType("datetime");
                entity.HasOne(d => d.Patient).WithMany(p => p.Visitations)
                    .HasForeignKey(d => d.PatientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Visitations_Patients");
                entity.HasOne(d => d.Doctor).WithMany(p => p.Visitations)
                    .HasForeignKey(d => d.DoctorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Visitations_Doctors");
            });*/
        }
    }
}
