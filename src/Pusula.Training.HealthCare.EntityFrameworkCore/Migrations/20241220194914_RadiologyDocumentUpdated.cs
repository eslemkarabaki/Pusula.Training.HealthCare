using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pusula.Training.HealthCare.Migrations
{
    /// <inheritdoc />
    public partial class RadiologyDocumentUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppRadiologyExaminationDocuments_AppRadiologyExaminationPro~",
                table: "AppRadiologyExaminationDocuments");

            migrationBuilder.DropColumn(
                name: "DocumentName",
                table: "AppRadiologyExaminationDocuments");

            migrationBuilder.RenameColumn(
                name: "RadiologyExaminationProcedureId",
                table: "AppRadiologyExaminationDocuments",
                newName: "ItemId");

            migrationBuilder.RenameColumn(
                name: "DocumentPath",
                table: "AppRadiologyExaminationDocuments",
                newName: "Path");

            migrationBuilder.RenameIndex(
                name: "IX_AppRadiologyExaminationDocuments_RadiologyExaminationProced~",
                table: "AppRadiologyExaminationDocuments",
                newName: "IX_AppRadiologyExaminationDocuments_ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_AppRadiologyExaminations_Name",
                table: "AppRadiologyExaminations",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AppRadiologyExaminationDocuments_AppRadiologyRequestItems_I~",
                table: "AppRadiologyExaminationDocuments",
                column: "ItemId",
                principalTable: "AppRadiologyRequestItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppRadiologyExaminationDocuments_AppRadiologyRequestItems_I~",
                table: "AppRadiologyExaminationDocuments");

            migrationBuilder.DropIndex(
                name: "IX_AppRadiologyExaminations_Name",
                table: "AppRadiologyExaminations");

            migrationBuilder.RenameColumn(
                name: "Path",
                table: "AppRadiologyExaminationDocuments",
                newName: "DocumentPath");

            migrationBuilder.RenameColumn(
                name: "ItemId",
                table: "AppRadiologyExaminationDocuments",
                newName: "RadiologyExaminationProcedureId");

            migrationBuilder.RenameIndex(
                name: "IX_AppRadiologyExaminationDocuments_ItemId",
                table: "AppRadiologyExaminationDocuments",
                newName: "IX_AppRadiologyExaminationDocuments_RadiologyExaminationProced~");

            migrationBuilder.AddColumn<string>(
                name: "DocumentName",
                table: "AppRadiologyExaminationDocuments",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_AppRadiologyExaminationDocuments_AppRadiologyExaminationPro~",
                table: "AppRadiologyExaminationDocuments",
                column: "RadiologyExaminationProcedureId",
                principalTable: "AppRadiologyExaminationProcedures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
