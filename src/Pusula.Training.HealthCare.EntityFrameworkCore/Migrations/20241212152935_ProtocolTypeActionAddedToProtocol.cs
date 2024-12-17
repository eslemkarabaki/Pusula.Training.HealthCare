using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pusula.Training.HealthCare.Migrations
{
    /// <inheritdoc />
    public partial class ProtocolTypeActionAddedToProtocol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProtocolTypeActionId",
                table: "AppProtocols",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_AppProtocols_ProtocolTypeActionId",
                table: "AppProtocols",
                column: "ProtocolTypeActionId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppProtocols_AppProtocolTypeActions_ProtocolTypeActionId",
                table: "AppProtocols",
                column: "ProtocolTypeActionId",
                principalTable: "AppProtocolTypeActions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppProtocols_AppProtocolTypeActions_ProtocolTypeActionId",
                table: "AppProtocols");

            migrationBuilder.DropIndex(
                name: "IX_AppProtocols_ProtocolTypeActionId",
                table: "AppProtocols");

            migrationBuilder.DropColumn(
                name: "ProtocolTypeActionId",
                table: "AppProtocols");
        }
    }
}
