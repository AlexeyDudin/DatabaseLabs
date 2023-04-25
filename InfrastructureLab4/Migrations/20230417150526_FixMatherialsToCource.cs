using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfrastructureLab4.Migrations
{
    /// <inheritdoc />
    public partial class FixMatherialsToCource : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cource_matherial_cources_ModuleId",
                table: "cource_matherial");

            migrationBuilder.CreateIndex(
                name: "IX_cource_module_status_EnrollmentId",
                table: "cource_module_status",
                column: "EnrollmentId");

            migrationBuilder.CreateIndex(
                name: "IX_cource_module_status_ModuleId",
                table: "cource_module_status",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_cource_matherial_CourceId",
                table: "cource_matherial",
                column: "CourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_cource_matherial_cources_CourceId",
                table: "cource_matherial",
                column: "CourceId",
                principalTable: "cources",
                principalColumn: "column_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cource_matherial_cources_CourceId",
                table: "cource_matherial");

            migrationBuilder.DropIndex(
                name: "IX_cource_module_status_EnrollmentId",
                table: "cource_module_status");

            migrationBuilder.DropIndex(
                name: "IX_cource_module_status_ModuleId",
                table: "cource_module_status");

            migrationBuilder.DropIndex(
                name: "IX_cource_matherial_CourceId",
                table: "cource_matherial");

            migrationBuilder.AddForeignKey(
                name: "FK_cource_matherial_cources_ModuleId",
                table: "cource_matherial",
                column: "ModuleId",
                principalTable: "cources",
                principalColumn: "column_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
