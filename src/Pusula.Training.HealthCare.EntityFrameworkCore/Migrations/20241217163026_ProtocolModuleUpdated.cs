using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pusula.Training.HealthCare.Migrations
{
    /// <inheritdoc />
    public partial class ProtocolModuleUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Allergies",
                table: "AppExamination");

            migrationBuilder.DropColumn(
                name: "ChronicDiseases",
                table: "AppExamination");

            migrationBuilder.DropColumn(
                name: "Diagnosis",
                table: "AppExamination");

            migrationBuilder.DropColumn(
                name: "IdentityNumber",
                table: "AppExamination");

            migrationBuilder.DropColumn(
                name: "ImagingResults",
                table: "AppExamination");

            migrationBuilder.DropColumn(
                name: "Medications",
                table: "AppExamination");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "AppExamination");

            migrationBuilder.DropColumn(
                name: "Prescription",
                table: "AppExamination");

            migrationBuilder.RenameColumn(
                name: "VisitDate",
                table: "AppExamination",
                newName: "StartDate");

            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                table: "AppProtocolTypes",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "AppProtocolTypes",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AppProtocolTypes",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "DoctorId",
                table: "AppExamination",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ProtocolId",
                table: "AppExamination",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "SummaryDocument",
                table: "AppExamination",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ExaminationAnamnez",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ExaminationId = table.Column<Guid>(type: "uuid", nullable: false),
                    Complaint = table.Column<string>(type: "text", nullable: false),
                    History = table.Column<string>(type: "text", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExaminationAnamnez", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExaminationDiagnoses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ExaminationId = table.Column<Guid>(type: "uuid", nullable: false),
                    DiagnosisId = table.Column<Guid>(type: "uuid", nullable: false),
                    Explanation = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExaminationDiagnoses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExaminationPhysical",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ExaminationId = table.Column<Guid>(type: "uuid", nullable: false),
                    Weight = table.Column<float>(type: "real", nullable: true),
                    Height = table.Column<float>(type: "real", nullable: true),
                    BodyMassIndex = table.Column<float>(type: "real", nullable: true),
                    VitalAge = table.Column<float>(type: "real", nullable: true),
                    Fever = table.Column<float>(type: "real", nullable: true),
                    Pulse = table.Column<float>(type: "real", nullable: true),
                    SystolicBloodPressure = table.Column<float>(type: "real", nullable: true),
                    DiastolicBloodPressure = table.Column<float>(type: "real", nullable: true),
                    SPO2 = table.Column<float>(type: "real", nullable: true),
                    PhysicalNote = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExaminationPhysical", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExaminationAnamnez");

            migrationBuilder.DropTable(
                name: "ExaminationDiagnoses");

            migrationBuilder.DropTable(
                name: "ExaminationPhysical");

            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "AppProtocolTypes");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "AppProtocolTypes");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AppProtocolTypes");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "AppExamination");

            migrationBuilder.DropColumn(
                name: "ProtocolId",
                table: "AppExamination");

            migrationBuilder.DropColumn(
                name: "SummaryDocument",
                table: "AppExamination");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "AppExamination",
                newName: "VisitDate");

            migrationBuilder.AddColumn<string>(
                name: "Allergies",
                table: "AppExamination",
                type: "character varying(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ChronicDiseases",
                table: "AppExamination",
                type: "character varying(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Diagnosis",
                table: "AppExamination",
                type: "character varying(128)",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdentityNumber",
                table: "AppExamination",
                type: "character varying(11)",
                maxLength: 11,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImagingResults",
                table: "AppExamination",
                type: "character varying(2)",
                maxLength: 2,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Medications",
                table: "AppExamination",
                type: "character varying(128)",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "AppExamination",
                type: "character varying(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Prescription",
                table: "AppExamination",
                type: "character varying(128)",
                maxLength: 128,
                nullable: true);
        }
    }
}
