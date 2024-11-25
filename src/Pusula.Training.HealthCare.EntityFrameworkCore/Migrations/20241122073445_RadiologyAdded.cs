using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pusula.Training.HealthCare.Migrations
{
    /// <inheritdoc />
    public partial class RadiologyAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppRadiologyExaminationGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRadiologyExaminationGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppRadiologyExaminations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    ExaminationCode = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    GroupId = table.Column<Guid>(type: "uuid", nullable: false),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRadiologyExaminations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppRadiologyExaminations_AppRadiologyExaminationGroups_Grou~",
                        column: x => x.GroupId,
                        principalTable: "AppRadiologyExaminationGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppRadiologyExaminationProcedures",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Result = table.Column<string>(type: "text", nullable: false),
                    ResultDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DoctorId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProtocolId = table.Column<Guid>(type: "uuid", nullable: false),
                    RadiologyExaminationId = table.Column<Guid>(type: "uuid", nullable: false),
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
                    table.PrimaryKey("PK_AppRadiologyExaminationProcedures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppRadiologyExaminationProcedures_AppDoctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "AppDoctors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppRadiologyExaminationProcedures_AppProtocols_ProtocolId",
                        column: x => x.ProtocolId,
                        principalTable: "AppProtocols",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppRadiologyExaminationProcedures_AppRadiologyExaminations_~",
                        column: x => x.RadiologyExaminationId,
                        principalTable: "AppRadiologyExaminations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppRadiologyExaminationDocuments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DocumentName = table.Column<string>(type: "text", nullable: false),
                    DocumentPath = table.Column<string>(type: "text", nullable: false),
                    UploadDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    RadiologyExaminationProcedureId = table.Column<Guid>(type: "uuid", nullable: false),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRadiologyExaminationDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppRadiologyExaminationDocuments_AppRadiologyExaminationPro~",
                        column: x => x.RadiologyExaminationProcedureId,
                        principalTable: "AppRadiologyExaminationProcedures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppRadiologyExaminationDocuments_Id",
                table: "AppRadiologyExaminationDocuments",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_AppRadiologyExaminationDocuments_RadiologyExaminationProced~",
                table: "AppRadiologyExaminationDocuments",
                column: "RadiologyExaminationProcedureId");

            migrationBuilder.CreateIndex(
                name: "IX_AppRadiologyExaminationGroups_Id",
                table: "AppRadiologyExaminationGroups",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_AppRadiologyExaminationProcedures_DoctorId",
                table: "AppRadiologyExaminationProcedures",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_AppRadiologyExaminationProcedures_Id",
                table: "AppRadiologyExaminationProcedures",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_AppRadiologyExaminationProcedures_ProtocolId",
                table: "AppRadiologyExaminationProcedures",
                column: "ProtocolId");

            migrationBuilder.CreateIndex(
                name: "IX_AppRadiologyExaminationProcedures_RadiologyExaminationId",
                table: "AppRadiologyExaminationProcedures",
                column: "RadiologyExaminationId");

            migrationBuilder.CreateIndex(
                name: "IX_AppRadiologyExaminations_GroupId",
                table: "AppRadiologyExaminations",
                column: "GroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppRadiologyExaminationDocuments");

            migrationBuilder.DropTable(
                name: "AppRadiologyExaminationProcedures");

            migrationBuilder.DropTable(
                name: "AppRadiologyExaminations");

            migrationBuilder.DropTable(
                name: "AppRadiologyExaminationGroups");
        }
    }
}
