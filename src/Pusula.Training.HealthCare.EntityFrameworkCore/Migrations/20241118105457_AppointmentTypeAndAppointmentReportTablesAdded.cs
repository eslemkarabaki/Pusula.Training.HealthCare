using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pusula.Training.HealthCare.Migrations
{
    /// <inheritdoc />
    public partial class AppointmentTypeAndAppointmentReportTablesAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppAppointments_AppHospitals_HospitalId",
                table: "AppAppointments");

            migrationBuilder.RenameColumn(
                name: "HospitalId",
                table: "AppAppointments",
                newName: "AppointmentTypeId");

            migrationBuilder.RenameColumn(
                name: "AppointmentDate",
                table: "AppAppointments",
                newName: "AppointmentStartDate");

            migrationBuilder.RenameIndex(
                name: "IX_AppAppointments_HospitalId",
                table: "AppAppointments",
                newName: "IX_AppAppointments_AppointmentTypeId");

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "AppAppointments",
                type: "character varying(128)",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AppointmentEndDate",
                table: "AppAppointments",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "AppAppointmentReports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ReportDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    PriorityNotes = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    DoctorNotes = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    AppointmentId = table.Column<Guid>(type: "uuid", nullable: false),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppAppointmentReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppAppointmentReports_AppAppointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "AppAppointments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppAppointmentTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppAppointmentTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppAppointmentReports_AppointmentId",
                table: "AppAppointmentReports",
                column: "AppointmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppAppointments_AppHospitals_AppointmentTypeId",
                table: "AppAppointments",
                column: "AppointmentTypeId",
                principalTable: "AppHospitals",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppAppointments_AppHospitals_AppointmentTypeId",
                table: "AppAppointments");

            migrationBuilder.DropTable(
                name: "AppAppointmentReports");

            migrationBuilder.DropTable(
                name: "AppAppointmentTypes");

            migrationBuilder.DropColumn(
                name: "AppointmentEndDate",
                table: "AppAppointments");

            migrationBuilder.RenameColumn(
                name: "AppointmentTypeId",
                table: "AppAppointments",
                newName: "HospitalId");

            migrationBuilder.RenameColumn(
                name: "AppointmentStartDate",
                table: "AppAppointments",
                newName: "AppointmentDate");

            migrationBuilder.RenameIndex(
                name: "IX_AppAppointments_AppointmentTypeId",
                table: "AppAppointments",
                newName: "IX_AppAppointments_HospitalId");

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "AppAppointments",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(128)",
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AppAppointments_AppHospitals_HospitalId",
                table: "AppAppointments",
                column: "HospitalId",
                principalTable: "AppHospitals",
                principalColumn: "Id");
        }
    }
}
