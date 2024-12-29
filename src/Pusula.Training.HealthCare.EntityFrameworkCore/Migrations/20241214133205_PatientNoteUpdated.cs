using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pusula.Training.HealthCare.Migrations
{
    /// <inheritdoc />
    public partial class PatientNoteUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppPatientNotes_AbpUsers_DeleterId",
                table: "AppPatientNotes");

            migrationBuilder.DropForeignKey(
                name: "FK_AppPatientNotes_AbpUsers_LastModifierId",
                table: "AppPatientNotes");

            migrationBuilder.DropIndex(
                name: "IX_AppPatientNotes_DeleterId",
                table: "AppPatientNotes");

            migrationBuilder.DropIndex(
                name: "IX_AppPatientNotes_LastModifierId",
                table: "AppPatientNotes");

            migrationBuilder.RenameColumn(
                name: "WorkingHours",
                table: "AppDoctors",
                newName: "AppointmentTime");

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "AppAddresses",
                type: "character varying(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                table: "AppAddresses",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "AppAddresses",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExtraProperties",
                table: "AppAddresses",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AppAddresses",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "AppAddresses");

            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "AppAddresses");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "AppAddresses");

            migrationBuilder.DropColumn(
                name: "ExtraProperties",
                table: "AppAddresses");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AppAddresses");

            migrationBuilder.RenameColumn(
                name: "AppointmentTime",
                table: "AppDoctors",
                newName: "WorkingHours");

            migrationBuilder.CreateIndex(
                name: "IX_AppPatientNotes_DeleterId",
                table: "AppPatientNotes",
                column: "DeleterId");

            migrationBuilder.CreateIndex(
                name: "IX_AppPatientNotes_LastModifierId",
                table: "AppPatientNotes",
                column: "LastModifierId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppPatientNotes_AbpUsers_DeleterId",
                table: "AppPatientNotes",
                column: "DeleterId",
                principalTable: "AbpUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppPatientNotes_AbpUsers_LastModifierId",
                table: "AppPatientNotes",
                column: "LastModifierId",
                principalTable: "AbpUsers",
                principalColumn: "Id");
        }
    }
}
