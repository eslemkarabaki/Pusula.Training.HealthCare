using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pusula.Training.HealthCare.Migrations
{
    /// <inheritdoc />
    public partial class RelationHasBeenAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_AppExamination_DoctorId",
                table: "AppExamination",
                column: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppExamination_AppDoctors_DoctorId",
                table: "AppExamination",
                column: "DoctorId",
                principalTable: "AppDoctors",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppExamination_AppDoctors_DoctorId",
                table: "AppExamination");

            migrationBuilder.DropIndex(
                name: "IX_AppExamination_DoctorId",
                table: "AppExamination");
        }
    }
}
