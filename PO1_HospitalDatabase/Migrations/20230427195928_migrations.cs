using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PO1_HospitalDatabase.Migrations
{
    /// <inheritdoc />
    public partial class migrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "doctors",
                columns: table => new
                {
                    DoctorId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Specialty = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Doctor__", x => x.DoctorId);
                });

            migrationBuilder.CreateTable(
                name: "medicaments",
                columns: table => new
                {
                    MedicamentId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Medicaments__", x => x.MedicamentId);
                });

            migrationBuilder.CreateTable(
                name: "patients",
                columns: table => new
                {
                    PatientId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Adress = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 80, nullable: false),
                    HasInsurance = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Patients__", x => x.PatientId);
                });

            migrationBuilder.CreateTable(
                name: "diagnoses",
                columns: table => new
                {
                    DiagnoseId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DiagnoseName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Comments = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false),
                    PatientId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Diagnose", x => x.DiagnoseId);
                    table.ForeignKey(
                        name: "FK_Diagnose_Patients",
                        column: x => x.PatientId,
                        principalTable: "patients",
                        principalColumn: "PatientId");
                });

            migrationBuilder.CreateTable(
                name: "patientmedicaments",
                columns: table => new
                {
                    PatientId = table.Column<int>(type: "INTEGER", nullable: false),
                    MedicamentId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PatientMedicaments", x => new { x.PatientId, x.MedicamentId });
                    table.ForeignKey(
                        name: "FK_PatientMedicament_Medicaments",
                        column: x => x.MedicamentId,
                        principalTable: "medicaments",
                        principalColumn: "MedicamentId");
                    table.ForeignKey(
                        name: "FK_PatientMedicament_Patients",
                        column: x => x.PatientId,
                        principalTable: "patients",
                        principalColumn: "PatientId");
                });

            migrationBuilder.CreateTable(
                name: "visitations",
                columns: table => new
                {
                    VisitationId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Comments = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(now())"),
                    PatientId = table.Column<int>(type: "INTEGER", nullable: false),
                    DoctorId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Visistations__", x => x.VisitationId);
                    table.ForeignKey(
                        name: "FK_Visitations_Doctors",
                        column: x => x.DoctorId,
                        principalTable: "doctors",
                        principalColumn: "DoctorId");
                    table.ForeignKey(
                        name: "FK_Visitations_Patients",
                        column: x => x.PatientId,
                        principalTable: "patients",
                        principalColumn: "PatientId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_diagnoses_PatientId",
                table: "diagnoses",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_patientmedicaments_MedicamentId",
                table: "patientmedicaments",
                column: "MedicamentId");

            migrationBuilder.CreateIndex(
                name: "IX_visitations_DoctorId",
                table: "visitations",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_visitations_PatientId",
                table: "visitations",
                column: "PatientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "diagnoses");

            migrationBuilder.DropTable(
                name: "patientmedicaments");

            migrationBuilder.DropTable(
                name: "visitations");

            migrationBuilder.DropTable(
                name: "medicaments");

            migrationBuilder.DropTable(
                name: "doctors");

            migrationBuilder.DropTable(
                name: "patients");
        }
    }
}
