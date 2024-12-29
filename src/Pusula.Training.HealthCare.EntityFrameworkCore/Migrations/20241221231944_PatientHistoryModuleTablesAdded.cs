using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pusula.Training.HealthCare.Migrations
{
    /// <inheritdoc />
    public partial class PatientHistoryModuleTablesAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppAllergies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
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
                    table.PrimaryKey("PK_AppAllergies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppBloodTransfusions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
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
                    table.PrimaryKey("PK_AppBloodTransfusions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppEducations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
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
                    table.PrimaryKey("PK_AppEducations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppJobs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
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
                    table.PrimaryKey("PK_AppJobs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppMedicines",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
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
                    table.PrimaryKey("PK_AppMedicines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppOperations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
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
                    table.PrimaryKey("PK_AppOperations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppVaccines",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
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
                    table.PrimaryKey("PK_AppVaccines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppPatientHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PatientId = table.Column<Guid>(type: "uuid", nullable: false),
                    JobId = table.Column<Guid>(type: "uuid", nullable: false),
                    EducationId = table.Column<Guid>(type: "uuid", nullable: false),
                    Habits = table.Column<int[]>(type: "integer[]", nullable: false),
                    SpecialCases = table.Column<int[]>(type: "integer[]", nullable: false),
                    BodyDevices = table.Column<int[]>(type: "integer[]", nullable: false),
                    Therapies = table.Column<int[]>(type: "integer[]", nullable: false),
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
                    table.PrimaryKey("PK_AppPatientHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppPatientHistories_AppEducations_EducationId",
                        column: x => x.EducationId,
                        principalTable: "AppEducations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppPatientHistories_AppJobs_JobId",
                        column: x => x.JobId,
                        principalTable: "AppJobs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppPatientHistories_AppPatients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "AppPatients",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppPatientHistoryAllergies",
                columns: table => new
                {
                    PatientHistoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    AllergyId = table.Column<Guid>(type: "uuid", nullable: false),
                    Explanation = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    NotExist = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppPatientHistoryAllergies", x => new { x.AllergyId, x.PatientHistoryId });
                    table.ForeignKey(
                        name: "FK_AppPatientHistoryAllergies_AppAllergies_AllergyId",
                        column: x => x.AllergyId,
                        principalTable: "AppAllergies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppPatientHistoryAllergies_AppPatientHistories_AllergyId",
                        column: x => x.AllergyId,
                        principalTable: "AppPatientHistories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppPatientHistoryBloodTransfusions",
                columns: table => new
                {
                    PatientHistoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    BloodTransfusionId = table.Column<Guid>(type: "uuid", nullable: false),
                    Explanation = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    NotExist = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppPatientHistoryBloodTransfusions", x => new { x.BloodTransfusionId, x.PatientHistoryId });
                    table.ForeignKey(
                        name: "FK_AppPatientHistoryBloodTransfusions_AppBloodTransfusions_Blo~",
                        column: x => x.BloodTransfusionId,
                        principalTable: "AppBloodTransfusions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppPatientHistoryBloodTransfusions_AppPatientHistories_Bloo~",
                        column: x => x.BloodTransfusionId,
                        principalTable: "AppPatientHistories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppPatientHistoryMedicines",
                columns: table => new
                {
                    PatientHistoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    MedicineId = table.Column<Guid>(type: "uuid", nullable: false),
                    Explanation = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    NotExist = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppPatientHistoryMedicines", x => new { x.MedicineId, x.PatientHistoryId });
                    table.ForeignKey(
                        name: "FK_AppPatientHistoryMedicines_AppMedicines_MedicineId",
                        column: x => x.MedicineId,
                        principalTable: "AppMedicines",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppPatientHistoryMedicines_AppPatientHistories_MedicineId",
                        column: x => x.MedicineId,
                        principalTable: "AppPatientHistories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppPatientHistoryOperations",
                columns: table => new
                {
                    PatientHistoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    OperationId = table.Column<Guid>(type: "uuid", nullable: false),
                    Explanation = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    NotExist = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppPatientHistoryOperations", x => new { x.OperationId, x.PatientHistoryId });
                    table.ForeignKey(
                        name: "FK_AppPatientHistoryOperations_AppOperations_OperationId",
                        column: x => x.OperationId,
                        principalTable: "AppOperations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppPatientHistoryOperations_AppPatientHistories_OperationId",
                        column: x => x.OperationId,
                        principalTable: "AppPatientHistories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppPatientHistoryVaccines",
                columns: table => new
                {
                    PatientHistoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    VaccineId = table.Column<Guid>(type: "uuid", nullable: false),
                    Explanation = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    NotExist = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppPatientHistoryVaccines", x => new { x.VaccineId, x.PatientHistoryId });
                    table.ForeignKey(
                        name: "FK_AppPatientHistoryVaccines_AppPatientHistories_VaccineId",
                        column: x => x.VaccineId,
                        principalTable: "AppPatientHistories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppPatientHistoryVaccines_AppVaccines_VaccineId",
                        column: x => x.VaccineId,
                        principalTable: "AppVaccines",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppPatientHistories_EducationId",
                table: "AppPatientHistories",
                column: "EducationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppPatientHistories_JobId",
                table: "AppPatientHistories",
                column: "JobId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppPatientHistories_PatientId",
                table: "AppPatientHistories",
                column: "PatientId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppPatientHistoryAllergies");

            migrationBuilder.DropTable(
                name: "AppPatientHistoryBloodTransfusions");

            migrationBuilder.DropTable(
                name: "AppPatientHistoryMedicines");

            migrationBuilder.DropTable(
                name: "AppPatientHistoryOperations");

            migrationBuilder.DropTable(
                name: "AppPatientHistoryVaccines");

            migrationBuilder.DropTable(
                name: "AppAllergies");

            migrationBuilder.DropTable(
                name: "AppBloodTransfusions");

            migrationBuilder.DropTable(
                name: "AppMedicines");

            migrationBuilder.DropTable(
                name: "AppOperations");

            migrationBuilder.DropTable(
                name: "AppPatientHistories");

            migrationBuilder.DropTable(
                name: "AppVaccines");

            migrationBuilder.DropTable(
                name: "AppEducations");

            migrationBuilder.DropTable(
                name: "AppJobs");
        }
    }
}
