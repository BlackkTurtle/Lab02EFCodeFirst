using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PO1_HospitalDatabase.Data.Models;

namespace PO1_HospitalDatabase.Data
{
    public class HospitalContext:DbContext
    {
        public HospitalContext(DbContextOptions<HospitalContext> options) : base(options)
        {

        }
        public DbSet<Diagnose> diagnoses =>Set<Diagnose>();
        public DbSet<Doctor> doctors => Set<Doctor>();
        public DbSet<Medicaments> medicaments =>Set<Medicaments>();
        public DbSet<PatientMedicament> patientmedicaments =>Set<PatientMedicament>();
        public DbSet<Patients> patients =>Set<Patients>();
        public DbSet<Visitations> visitations =>Set<Visitations>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Diagnose>(entity =>
            {
                entity.HasKey(e => e.DiagnoseId).HasName("PK__Diagnose");
                entity.Property(e => e.DiagnoseName).HasMaxLength(50);
                entity.Property(e => e.Comments).HasMaxLength(250);
                entity.HasOne(d => d.Patient).WithMany(p => p.Diagnoses)
                    .HasForeignKey(d => d.PatientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Diagnose_Patients");
            });

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.HasKey(e => e.DoctorId).HasName("PK__Doctor__");

                entity.Property(e => e.Name).HasMaxLength(100);
                entity.Property(e => e.Specialty).HasMaxLength(100);
            });

            modelBuilder.Entity<Medicaments>(entity =>
            {
                entity.HasKey(e => e.MedicamentId).HasName("PK__Medicaments__");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<PatientMedicament>(entity =>
            {
                entity.HasKey(e => new { e.PatientId, e.MedicamentId }).HasName("PK__PatientMedicaments");
                entity.HasOne(d => d.Patient).WithMany(p => p.PatientMedicaments)
                    .HasForeignKey(d => d.PatientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PatientMedicament_Patients");
                entity.HasOne(d => d.Medicament).WithMany(p => p.PatientMedicaments)
                    .HasForeignKey(d => d.MedicamentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PatientMedicament_Medicaments");
            });

            modelBuilder.Entity<Patients>(entity =>
            {
                entity.HasKey(e => e.PatientId).HasName("PK__Patients__");

                entity.Property(e => e.FirstName).HasMaxLength(50);
                entity.Property(e => e.LastName).HasMaxLength(50);
                entity.Property(e => e.Adress).HasMaxLength(250);
                entity.Property(e => e.Email).HasMaxLength(80);
                entity.Property(e => e.HasInsurance);
            });

            modelBuilder.Entity<Visitations>(entity =>
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
            });
        }
    }
}
