using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pusula.Training.HealthCare.Migrations
{
    /// <inheritdoc />
    public partial class ExaminationUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ExaminationPhysical_ExaminationId",
                table: "ExaminationPhysical",
                column: "ExaminationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExaminationDiagnoses_ExaminationId",
                table: "ExaminationDiagnoses",
                column: "ExaminationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExaminationAnamnez_ExaminationId",
                table: "ExaminationAnamnez",
                column: "ExaminationId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ExaminationAnamnez_AppExamination_ExaminationId",
                table: "ExaminationAnamnez",
                column: "ExaminationId",
                principalTable: "AppExamination",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExaminationDiagnoses_AppExamination_ExaminationId",
                table: "ExaminationDiagnoses",
                column: "ExaminationId",
                principalTable: "AppExamination",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExaminationPhysical_AppExamination_ExaminationId",
                table: "ExaminationPhysical",
                column: "ExaminationId",
                principalTable: "AppExamination",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExaminationAnamnez_AppExamination_ExaminationId",
                table: "ExaminationAnamnez");

            migrationBuilder.DropForeignKey(
                name: "FK_ExaminationDiagnoses_AppExamination_ExaminationId",
                table: "ExaminationDiagnoses");

            migrationBuilder.DropForeignKey(
                name: "FK_ExaminationPhysical_AppExamination_ExaminationId",
                table: "ExaminationPhysical");

            migrationBuilder.DropIndex(
                name: "IX_ExaminationPhysical_ExaminationId",
                table: "ExaminationPhysical");

            migrationBuilder.DropIndex(
                name: "IX_ExaminationDiagnoses_ExaminationId",
                table: "ExaminationDiagnoses");

            migrationBuilder.DropIndex(
                name: "IX_ExaminationAnamnez_ExaminationId",
                table: "ExaminationAnamnez");
        }
    }
}
