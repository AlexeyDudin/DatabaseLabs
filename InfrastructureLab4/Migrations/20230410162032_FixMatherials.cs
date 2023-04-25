using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfrastructureLab4.Migrations
{
    /// <inheritdoc />
    public partial class FixMatherials : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cource_enrollments_cource_module_status_EnrollmentId",
                table: "cource_enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_cource_matherial_cource_module_status_ModuleId",
                table: "cource_matherial");

            migrationBuilder.DropPrimaryKey(
                name: "PK_cource_module_status",
                table: "cource_module_status");

            migrationBuilder.AddPrimaryKey(
                name: "PK_cource_module_status",
                table: "cource_module_status",
                column: "EnrollmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_cource_enrollments_cource_module_status_EnrollmentId",
                table: "cource_enrollments",
                column: "EnrollmentId",
                principalTable: "cource_module_status",
                principalColumn: "EnrollmentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_cource_matherial_cource_module_status_ModuleId",
                table: "cource_matherial",
                column: "ModuleId",
                principalTable: "cource_module_status",
                principalColumn: "EnrollmentId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cource_enrollments_cource_module_status_EnrollmentId",
                table: "cource_enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_cource_matherial_cource_module_status_ModuleId",
                table: "cource_matherial");

            migrationBuilder.DropPrimaryKey(
                name: "PK_cource_module_status",
                table: "cource_module_status");

            migrationBuilder.AddPrimaryKey(
                name: "PK_cource_module_status",
                table: "cource_module_status",
                column: "ModuleId");

            migrationBuilder.AddForeignKey(
                name: "FK_cource_enrollments_cource_module_status_EnrollmentId",
                table: "cource_enrollments",
                column: "EnrollmentId",
                principalTable: "cource_module_status",
                principalColumn: "ModuleId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_cource_matherial_cource_module_status_ModuleId",
                table: "cource_matherial",
                column: "ModuleId",
                principalTable: "cource_module_status",
                principalColumn: "ModuleId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
