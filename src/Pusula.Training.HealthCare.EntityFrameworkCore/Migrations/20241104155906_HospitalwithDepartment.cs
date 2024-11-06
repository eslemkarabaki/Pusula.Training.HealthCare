using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pusula.Training.HealthCare.Migrations
{
    /// <inheritdoc />
    public partial class HospitalwithDepartment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppHospitalDepartments_AppDepartments_DepartmentId",
                table: "AppHospitalDepartments");

            migrationBuilder.DropForeignKey(
                name: "FK_AppHospitalDepartments_AppHospitals_HospitalId",
                table: "AppHospitalDepartments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppHospitalDepartments",
                table: "AppHospitalDepartments");

            migrationBuilder.DropIndex(
                name: "IX_AppHospitalDepartments_HospitalId",
                table: "AppHospitalDepartments");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "AppHospitalDepartments");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppHospitalDepartments",
                table: "AppHospitalDepartments",
                columns: new[] { "HospitalId", "DepartmentId" });

            migrationBuilder.CreateIndex(
                name: "IX_AppHospitalDepartments_DepartmentId",
                table: "AppHospitalDepartments",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AppHospitalDepartments_HospitalId_DepartmentId",
                table: "AppHospitalDepartments",
                columns: new[] { "HospitalId", "DepartmentId" });

            migrationBuilder.AddForeignKey(
                name: "FK_AppHospitalDepartments_AppDepartments_DepartmentId",
                table: "AppHospitalDepartments",
                column: "DepartmentId",
                principalTable: "AppDepartments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppHospitalDepartments_AppHospitals_HospitalId",
                table: "AppHospitalDepartments",
                column: "HospitalId",
                principalTable: "AppHospitals",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppHospitalDepartments_AppDepartments_DepartmentId",
                table: "AppHospitalDepartments");

            migrationBuilder.DropForeignKey(
                name: "FK_AppHospitalDepartments_AppHospitals_HospitalId",
                table: "AppHospitalDepartments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppHospitalDepartments",
                table: "AppHospitalDepartments");

            migrationBuilder.DropIndex(
                name: "IX_AppHospitalDepartments_DepartmentId",
                table: "AppHospitalDepartments");

            migrationBuilder.DropIndex(
                name: "IX_AppHospitalDepartments_HospitalId_DepartmentId",
                table: "AppHospitalDepartments");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "AppHospitalDepartments",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppHospitalDepartments",
                table: "AppHospitalDepartments",
                columns: new[] { "DepartmentId", "HospitalId" });

            migrationBuilder.CreateIndex(
                name: "IX_AppHospitalDepartments_HospitalId",
                table: "AppHospitalDepartments",
                column: "HospitalId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppHospitalDepartments_AppDepartments_DepartmentId",
                table: "AppHospitalDepartments",
                column: "DepartmentId",
                principalTable: "AppDepartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppHospitalDepartments_AppHospitals_HospitalId",
                table: "AppHospitalDepartments",
                column: "HospitalId",
                principalTable: "AppHospitals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
