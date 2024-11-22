using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pusula.Training.HealthCare.Migrations
{
    /// <inheritdoc />
    public partial class CountryUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Abbreviation",
                table: "AppCountries");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AppCountries",
                type: "character varying(32)",
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(64)",
                oldMaxLength: 64);

            migrationBuilder.AddColumn<bool>(
                name: "IsCurrent",
                table: "AppCountries",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Iso",
                table: "AppCountries",
                type: "character varying(4)",
                maxLength: 4,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneCode",
                table: "AppCountries",
                type: "character varying(4)",
                maxLength: 4,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCurrent",
                table: "AppCountries");

            migrationBuilder.DropColumn(
                name: "Iso",
                table: "AppCountries");

            migrationBuilder.DropColumn(
                name: "PhoneCode",
                table: "AppCountries");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AppCountries",
                type: "character varying(64)",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(32)",
                oldMaxLength: 32);

            migrationBuilder.AddColumn<string>(
                name: "Abbreviation",
                table: "AppCountries",
                type: "character varying(8)",
                maxLength: 8,
                nullable: false,
                defaultValue: "");
        }
    }
}
