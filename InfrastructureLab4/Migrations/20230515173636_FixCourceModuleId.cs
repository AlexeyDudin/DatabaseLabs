using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfrastructureLab4.Migrations
{
    /// <inheritdoc />
    public partial class FixCourceModuleId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cource_enrollments_cource_module_status_EnrollmentId",
                table: "cource_enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_cource_matherial_cource_module_status_CourceModuleEnrollmentId",
                table: "cource_matherial");

            migrationBuilder.DropPrimaryKey(
                name: "PK_cource_module_status",
                table: "cource_module_status");

            migrationBuilder.DropIndex(
                name: "IX_cource_module_status_EnrollmentId",
                table: "cource_module_status");

            migrationBuilder.DropIndex(
                name: "IX_cource_module_status_ModuleId",
                table: "cource_module_status");

            migrationBuilder.DropIndex(
                name: "IX_cource_matherial_CourceModuleEnrollmentId",
                table: "cource_matherial");

            migrationBuilder.DropColumn(
                name: "CourceModuleEnrollmentId",
                table: "cource_matherial");

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "cource_module_status",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "EnrollmentKey",
                table: "cource_module_status",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "CourceModuleId",
                table: "cource_matherial",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CourceModuleId",
                table: "cource_enrollments",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddPrimaryKey(
                name: "PK_cource_module_status",
                table: "cource_module_status",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_cource_module_status_ModuleId",
                table: "cource_module_status",
                column: "ModuleId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_cource_enrollments_CourceModuleId",
                table: "cource_enrollments",
                column: "CourceModuleId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_cource_enrollments_cource_module_status_CourceModuleId",
                table: "cource_enrollments",
                column: "CourceModuleId",
                principalTable: "cource_module_status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_cource_module_status_cource_matherial_ModuleId",
                table: "cource_module_status",
                column: "ModuleId",
                principalTable: "cource_matherial",
                principalColumn: "ModuleId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cource_enrollments_cource_module_status_CourceModuleId",
                table: "cource_enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_cource_module_status_cource_matherial_ModuleId",
                table: "cource_module_status");

            migrationBuilder.DropPrimaryKey(
                name: "PK_cource_module_status",
                table: "cource_module_status");

            migrationBuilder.DropIndex(
                name: "IX_cource_module_status_ModuleId",
                table: "cource_module_status");

            migrationBuilder.DropIndex(
                name: "IX_cource_enrollments_CourceModuleId",
                table: "cource_enrollments");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "cource_module_status");

            migrationBuilder.DropColumn(
                name: "EnrollmentKey",
                table: "cource_module_status");

            migrationBuilder.DropColumn(
                name: "CourceModuleId",
                table: "cource_matherial");

            migrationBuilder.DropColumn(
                name: "CourceModuleId",
                table: "cource_enrollments");

            migrationBuilder.AddColumn<Guid>(
                name: "CourceModuleEnrollmentId",
                table: "cource_matherial",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_cource_module_status",
                table: "cource_module_status",
                column: "EnrollmentId");

            migrationBuilder.CreateIndex(
                name: "IX_cource_module_status_EnrollmentId",
                table: "cource_module_status",
                column: "EnrollmentId");

            migrationBuilder.CreateIndex(
                name: "IX_cource_module_status_ModuleId",
                table: "cource_module_status",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_cource_matherial_CourceModuleEnrollmentId",
                table: "cource_matherial",
                column: "CourceModuleEnrollmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_cource_enrollments_cource_module_status_EnrollmentId",
                table: "cource_enrollments",
                column: "EnrollmentId",
                principalTable: "cource_module_status",
                principalColumn: "EnrollmentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_cource_matherial_cource_module_status_CourceModuleEnrollmentId",
                table: "cource_matherial",
                column: "CourceModuleEnrollmentId",
                principalTable: "cource_module_status",
                principalColumn: "EnrollmentId");
        }
    }
}
