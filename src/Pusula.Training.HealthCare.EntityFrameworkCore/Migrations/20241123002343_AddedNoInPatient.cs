using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Pusula.Training.HealthCare.Migrations
{
    /// <inheritdoc />
    public partial class AddedNoInPatient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AppPatients_FirstName_LastName_IdentityNumber",
                table: "AppPatients");

            migrationBuilder.DeleteData(
                table: "AppAppDefaults",
                keyColumn: "Id",
                keyValue: new Guid("00c10a9c-cd0c-4987-8690-d7b1ea397ac4"));

            migrationBuilder.AddColumn<int>(
                name: "No",
                table: "AppPatients",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.InsertData(
                table: "AppAppDefaults",
                columns: new[] { "Id", "CreationTime", "CreatorId", "CurrentCountryId", "DeleterId", "DeletionTime", "LastModificationTime", "LastModifierId" },
                values: new object[] { new Guid("87e927c6-88c8-425c-8e3b-e9c0778d0b32"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, null, null });

            migrationBuilder.CreateIndex(
                name: "IX_AppPatients_FirstName_LastName_IdentityNumber_PassportNumber",
                table: "AppPatients",
                columns: new[] { "FirstName", "LastName", "IdentityNumber", "PassportNumber" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AppPatients_FirstName_LastName_IdentityNumber_PassportNumber",
                table: "AppPatients");

            migrationBuilder.DeleteData(
                table: "AppAppDefaults",
                keyColumn: "Id",
                keyValue: new Guid("87e927c6-88c8-425c-8e3b-e9c0778d0b32"));

            migrationBuilder.DropColumn(
                name: "No",
                table: "AppPatients");

            migrationBuilder.InsertData(
                table: "AppAppDefaults",
                columns: new[] { "Id", "CreationTime", "CreatorId", "CurrentCountryId", "DeleterId", "DeletionTime", "LastModificationTime", "LastModifierId" },
                values: new object[] { new Guid("00c10a9c-cd0c-4987-8690-d7b1ea397ac4"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, null, null });

            migrationBuilder.CreateIndex(
                name: "IX_AppPatients_FirstName_LastName_IdentityNumber",
                table: "AppPatients",
                columns: new[] { "FirstName", "LastName", "IdentityNumber" });
        }
    }
}
