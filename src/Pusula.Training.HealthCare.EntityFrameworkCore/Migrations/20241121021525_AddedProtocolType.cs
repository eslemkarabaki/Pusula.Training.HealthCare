using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pusula.Training.HealthCare.Migrations
{
    /// <inheritdoc />
    public partial class AddedProtocolType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "AppProtocols");
            
            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "AppProtocols");
            
            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "AppProtocols",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "AppProtocols",
                type: "character varying(512)",
                maxLength: 512,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DoctorId",
                table: "AppProtocols",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ProtocolTypeId",
                table: "AppProtocols",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "AppProtocols",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AppProtocolTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppProtocolTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppProtocols_DoctorId",
                table: "AppProtocols",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_AppProtocols_ProtocolTypeId",
                table: "AppProtocols",
                column: "ProtocolTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppProtocols_AppDoctors_DoctorId",
                table: "AppProtocols",
                column: "DoctorId",
                principalTable: "AppDoctors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppProtocols_AppProtocolTypes_ProtocolTypeId",
                table: "AppProtocols",
                column: "ProtocolTypeId",
                principalTable: "AppProtocolTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppProtocols_AppDoctors_DoctorId",
                table: "AppProtocols");

            migrationBuilder.DropForeignKey(
                name: "FK_AppProtocols_AppProtocolTypes_ProtocolTypeId",
                table: "AppProtocols");

            migrationBuilder.DropTable(
                name: "AppProtocolTypes");

            migrationBuilder.DropIndex(
                name: "IX_AppProtocols_DoctorId",
                table: "AppProtocols");

            migrationBuilder.DropIndex(
                name: "IX_AppProtocols_ProtocolTypeId",
                table: "AppProtocols");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "AppProtocols");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "AppProtocols");

            migrationBuilder.DropColumn(
                name: "ProtocolTypeId",
                table: "AppProtocols");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "AppProtocols");
            
            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "AppProtocols");
            
            migrationBuilder.AddColumn<string>(
                name: "EndTime",
                table: "AppProtocols",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "AppProtocols",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");
        }
    }
}
