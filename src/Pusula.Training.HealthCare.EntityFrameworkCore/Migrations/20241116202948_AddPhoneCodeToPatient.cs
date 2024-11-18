using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pusula.Training.HealthCare.Migrations
{
    /// <inheritdoc />
    public partial class AddPhoneCodeToPatient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HomePhoneNumberCode",
                table: "AppPatients",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MobilePhoneNumberCode",
                table: "AppPatients",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HomePhoneNumberCode",
                table: "AppPatients");

            migrationBuilder.DropColumn(
                name: "MobilePhoneNumberCode",
                table: "AppPatients");
        }
    }
}
