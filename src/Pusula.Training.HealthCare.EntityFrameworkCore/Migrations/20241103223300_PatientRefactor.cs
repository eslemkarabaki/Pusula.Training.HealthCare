using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pusula.Training.HealthCare.Migrations
{
    /// <inheritdoc />
    public partial class PatientRefactor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "HomePhoneNumber",
                table: "AppPatients",
                type: "character varying(32)",
                maxLength: 32,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BloodType",
                table: "AppPatients",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "CountryId",
                table: "AppPatients",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "MaritalStatus",
                table: "AppPatients",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "RegisterDate",
                table: "AppPatients",
                type: "timestamp without time zone",
                nullable: false,
                defaultValueSql: "NOW()");

            migrationBuilder.CreateTable(
                name: "AppCountries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Code = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
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
                    table.PrimaryKey("PK_AppCountries", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppPatients_CountryId",
                table: "AppPatients",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_AppPatients_FirstName_LastName_IdentityNumber",
                table: "AppPatients",
                columns: new[] { "FirstName", "LastName", "IdentityNumber" });

            migrationBuilder.AddForeignKey(
                name: "FK_AppPatients_AppCountries_CountryId",
                table: "AppPatients",
                column: "CountryId",
                principalTable: "AppCountries",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppPatients_AppCountries_CountryId",
                table: "AppPatients");

            migrationBuilder.DropTable(
                name: "AppCountries");

            migrationBuilder.DropIndex(
                name: "IX_AppPatients_CountryId",
                table: "AppPatients");

            migrationBuilder.DropIndex(
                name: "IX_AppPatients_FirstName_LastName_IdentityNumber",
                table: "AppPatients");

            migrationBuilder.DropColumn(
                name: "BloodType",
                table: "AppPatients");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "AppPatients");

            migrationBuilder.DropColumn(
                name: "MaritalStatus",
                table: "AppPatients");

            migrationBuilder.DropColumn(
                name: "RegisterDate",
                table: "AppPatients");

            migrationBuilder.AlterColumn<string>(
                name: "HomePhoneNumber",
                table: "AppPatients",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(32)",
                oldMaxLength: 32,
                oldNullable: true);
        }
    }
}
