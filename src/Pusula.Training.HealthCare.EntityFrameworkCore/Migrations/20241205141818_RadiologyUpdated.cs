using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pusula.Training.HealthCare.Migrations
{
    /// <inheritdoc />
    public partial class RadiologyUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppRadiologyRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RequestDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ProtocolId = table.Column<Guid>(type: "uuid", nullable: false),
                    DepartmentId = table.Column<Guid>(type: "uuid", nullable: false),
                    DoctorId = table.Column<Guid>(type: "uuid", nullable: false),
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
                    table.PrimaryKey("PK_AppRadiologyRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppRadiologyRequests_AppDepartments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "AppDepartments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppRadiologyRequests_AppDoctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "AppDoctors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppRadiologyRequests_AppProtocols_ProtocolId",
                        column: x => x.ProtocolId,
                        principalTable: "AppProtocols",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppRadiologyRequestItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RequestId = table.Column<Guid>(type: "uuid", nullable: false),
                    ExaminationId = table.Column<Guid>(type: "uuid", nullable: false),
                    Result = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    ResultDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    State = table.Column<int>(type: "integer", nullable: false),
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
                    table.PrimaryKey("PK_AppRadiologyRequestItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppRadiologyRequestItems_AppRadiologyExaminations_Examinati~",
                        column: x => x.ExaminationId,
                        principalTable: "AppRadiologyExaminations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppRadiologyRequestItems_AppRadiologyRequests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "AppRadiologyRequests",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppRadiologyRequestItems_ExaminationId",
                table: "AppRadiologyRequestItems",
                column: "ExaminationId");

            migrationBuilder.CreateIndex(
                name: "IX_AppRadiologyRequestItems_RequestId",
                table: "AppRadiologyRequestItems",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_AppRadiologyRequests_DepartmentId",
                table: "AppRadiologyRequests",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AppRadiologyRequests_DoctorId",
                table: "AppRadiologyRequests",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_AppRadiologyRequests_ProtocolId",
                table: "AppRadiologyRequests",
                column: "ProtocolId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppRadiologyRequestItems");

            migrationBuilder.DropTable(
                name: "AppRadiologyRequests");
        }
    }
}
