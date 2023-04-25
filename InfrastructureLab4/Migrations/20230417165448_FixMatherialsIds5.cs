using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfrastructureLab4.Migrations
{
    /// <inheritdoc />
    public partial class FixMatherialsIds5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_cource_matherial",
                table: "cource_matherial");

            migrationBuilder.AlterColumn<Guid>(
                name: "ModuleId",
                table: "cource_matherial",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "cource_module_id",
                table: "cource_matherial",
                column: "ModuleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "cource_module_id",
                table: "cource_matherial");

            migrationBuilder.AlterColumn<Guid>(
                name: "ModuleId",
                table: "cource_matherial",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddPrimaryKey(
                name: "PK_cource_matherial",
                table: "cource_matherial",
                column: "ModuleId");
        }
    }
}
