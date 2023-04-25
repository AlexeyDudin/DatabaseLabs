using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfrastructureLab4.Migrations
{
    /// <inheritdoc />
    public partial class FixMatherialsIds2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_cource_matherial_ModuleId",
                table: "cource_matherial",
                column: "ModuleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_cource_matherial_ModuleId",
                table: "cource_matherial");
        }
    }
}
