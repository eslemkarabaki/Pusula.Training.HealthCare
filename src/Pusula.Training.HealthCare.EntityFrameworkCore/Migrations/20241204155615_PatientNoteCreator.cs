using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pusula.Training.HealthCare.Migrations
{
    /// <inheritdoc />
    public partial class PatientNoteCreator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "AppPatientNotes",
                type: "character varying(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ExtraProperties",
                table: "AppPatientNotes",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_AppPatientNotes_CreatorId",
                table: "AppPatientNotes",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_AppPatientNotes_DeleterId",
                table: "AppPatientNotes",
                column: "DeleterId");

            migrationBuilder.CreateIndex(
                name: "IX_AppPatientNotes_LastModifierId",
                table: "AppPatientNotes",
                column: "LastModifierId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppPatientNotes_AbpUsers_CreatorId",
                table: "AppPatientNotes",
                column: "CreatorId",
                principalTable: "AbpUsers",
                principalColumn: "Id");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppPatientNotes_AbpUsers_CreatorId",
                table: "AppPatientNotes");

            migrationBuilder.DropForeignKey(
                name: "FK_AppPatientNotes_AbpUsers_DeleterId",
                table: "AppPatientNotes");

            migrationBuilder.DropForeignKey(
                name: "FK_AppPatientNotes_AbpUsers_LastModifierId",
                table: "AppPatientNotes");

            migrationBuilder.DropIndex(
                name: "IX_AppPatientNotes_CreatorId",
                table: "AppPatientNotes");

            migrationBuilder.DropIndex(
                name: "IX_AppPatientNotes_DeleterId",
                table: "AppPatientNotes");

            migrationBuilder.DropIndex(
                name: "IX_AppPatientNotes_LastModifierId",
                table: "AppPatientNotes");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "AppPatientNotes");

            migrationBuilder.DropColumn(
                name: "ExtraProperties",
                table: "AppPatientNotes");
        }
    }
}
