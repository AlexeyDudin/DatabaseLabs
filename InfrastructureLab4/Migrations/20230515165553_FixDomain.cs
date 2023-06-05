using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfrastructureLab4.Migrations
{
    /// <inheritdoc />
    public partial class FixDomain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cource_matherial_cource_module_status_ModuleId",
                table: "cource_matherial");

            migrationBuilder.AddColumn<Guid>(
                name: "CourceModuleEnrollmentId",
                table: "cource_matherial",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_cource_matherial_CourceModuleEnrollmentId",
                table: "cource_matherial",
                column: "CourceModuleEnrollmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_cource_matherial_cource_module_status_CourceModuleEnrollmentId",
                table: "cource_matherial",
                column: "CourceModuleEnrollmentId",
                principalTable: "cource_module_status",
                principalColumn: "EnrollmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cource_matherial_cource_module_status_CourceModuleEnrollmentId",
                table: "cource_matherial");

            migrationBuilder.DropIndex(
                name: "IX_cource_matherial_CourceModuleEnrollmentId",
                table: "cource_matherial");

            migrationBuilder.DropColumn(
                name: "CourceModuleEnrollmentId",
                table: "cource_matherial");

            migrationBuilder.AddForeignKey(
                name: "FK_cource_matherial_cource_module_status_ModuleId",
                table: "cource_matherial",
                column: "ModuleId",
                principalTable: "cource_module_status",
                principalColumn: "EnrollmentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
