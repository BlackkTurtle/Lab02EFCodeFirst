﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PO1_HospitalDatabase.Data;

#nullable disable

namespace PO1_HospitalDatabase.Migrations
{
    [DbContext(typeof(HospitalContext))]
    [Migration("20230427195928_migrations")]
    partial class migrations
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.5");

            modelBuilder.Entity("PO1_HospitalDatabase.Data.Models.Diagnose", b =>
                {
                    b.Property<int>("DiagnoseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Comments")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("TEXT");

                    b.Property<string>("DiagnoseName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<int>("PatientId")
                        .HasColumnType("INTEGER");

                    b.HasKey("DiagnoseId")
                        .HasName("PK__Diagnose");

                    b.HasIndex("PatientId");

                    b.ToTable("diagnoses");
                });

            modelBuilder.Entity("PO1_HospitalDatabase.Data.Models.Doctor", b =>
                {
                    b.Property<int>("DoctorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Specialty")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("DoctorId")
                        .HasName("PK__Doctor__");

                    b.ToTable("doctors");
                });

            modelBuilder.Entity("PO1_HospitalDatabase.Data.Models.Medicaments", b =>
                {
                    b.Property<int>("MedicamentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("MedicamentId")
                        .HasName("PK__Medicaments__");

                    b.ToTable("medicaments");
                });

            modelBuilder.Entity("PO1_HospitalDatabase.Data.Models.PatientMedicament", b =>
                {
                    b.Property<int>("PatientId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MedicamentId")
                        .HasColumnType("INTEGER");

                    b.HasKey("PatientId", "MedicamentId")
                        .HasName("PK__PatientMedicaments");

                    b.HasIndex("MedicamentId");

                    b.ToTable("patientmedicaments");
                });

            modelBuilder.Entity("PO1_HospitalDatabase.Data.Models.Patients", b =>
                {
                    b.Property<int>("PatientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Adress")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<bool>("HasInsurance")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("PatientId")
                        .HasName("PK__Patients__");

                    b.ToTable("patients");
                });

            modelBuilder.Entity("PO1_HospitalDatabase.Data.Models.Visitations", b =>
                {
                    b.Property<int>("VisitationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Comments")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Date")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(now())");

                    b.Property<int>("DoctorId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PatientId")
                        .HasColumnType("INTEGER");

                    b.HasKey("VisitationId")
                        .HasName("PK__Visistations__");

                    b.HasIndex("DoctorId");

                    b.HasIndex("PatientId");

                    b.ToTable("visitations");
                });

            modelBuilder.Entity("PO1_HospitalDatabase.Data.Models.Diagnose", b =>
                {
                    b.HasOne("PO1_HospitalDatabase.Data.Models.Patients", "Patient")
                        .WithMany("Diagnoses")
                        .HasForeignKey("PatientId")
                        .IsRequired()
                        .HasConstraintName("FK_Diagnose_Patients");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("PO1_HospitalDatabase.Data.Models.PatientMedicament", b =>
                {
                    b.HasOne("PO1_HospitalDatabase.Data.Models.Medicaments", "Medicament")
                        .WithMany("PatientMedicaments")
                        .HasForeignKey("MedicamentId")
                        .IsRequired()
                        .HasConstraintName("FK_PatientMedicament_Medicaments");

                    b.HasOne("PO1_HospitalDatabase.Data.Models.Patients", "Patient")
                        .WithMany("PatientMedicaments")
                        .HasForeignKey("PatientId")
                        .IsRequired()
                        .HasConstraintName("FK_PatientMedicament_Patients");

                    b.Navigation("Medicament");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("PO1_HospitalDatabase.Data.Models.Visitations", b =>
                {
                    b.HasOne("PO1_HospitalDatabase.Data.Models.Doctor", "Doctor")
                        .WithMany("Visitations")
                        .HasForeignKey("DoctorId")
                        .IsRequired()
                        .HasConstraintName("FK_Visitations_Doctors");

                    b.HasOne("PO1_HospitalDatabase.Data.Models.Patients", "Patient")
                        .WithMany("Visitations")
                        .HasForeignKey("PatientId")
                        .IsRequired()
                        .HasConstraintName("FK_Visitations_Patients");

                    b.Navigation("Doctor");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("PO1_HospitalDatabase.Data.Models.Doctor", b =>
                {
                    b.Navigation("Visitations");
                });

            modelBuilder.Entity("PO1_HospitalDatabase.Data.Models.Medicaments", b =>
                {
                    b.Navigation("PatientMedicaments");
                });

            modelBuilder.Entity("PO1_HospitalDatabase.Data.Models.Patients", b =>
                {
                    b.Navigation("Diagnoses");

                    b.Navigation("PatientMedicaments");

                    b.Navigation("Visitations");
                });
#pragma warning restore 612, 618
        }
    }
}