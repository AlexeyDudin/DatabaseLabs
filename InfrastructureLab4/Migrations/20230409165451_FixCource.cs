using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfrastructureLab4.Migrations
{
    /// <inheritdoc />
    public partial class FixCource : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cource_module_status",
                columns: table => new
                {
                    ModuleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EnrollmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Progress = table.Column<int>(type: "int", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cource_module_status", x => x.ModuleId);
                });

            migrationBuilder.CreateTable(
                name: "cource_status",
                columns: table => new
                {
                    EnrollmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Progress = table.Column<int>(type: "int", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cource_status", x => x.EnrollmentId);
                });

            migrationBuilder.CreateTable(
                name: "cources",
                columns: table => new
                {
                    column_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cources", x => x.column_id);
                });

            migrationBuilder.CreateTable(
                name: "cource_enrollments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnrollmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cource_enrollments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_cource_enrollments_cource_module_status_EnrollmentId",
                        column: x => x.EnrollmentId,
                        principalTable: "cource_module_status",
                        principalColumn: "ModuleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cource_enrollments_cource_status_EnrollmentId",
                        column: x => x.EnrollmentId,
                        principalTable: "cource_status",
                        principalColumn: "EnrollmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cource_enrollments_cources_CourceId",
                        column: x => x.CourceId,
                        principalTable: "cources",
                        principalColumn: "column_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cource_matherial",
                columns: table => new
                {
                    ModuleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsRequired = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cource_matherial", x => x.ModuleId);
                    table.ForeignKey(
                        name: "FK_cource_matherial_cource_module_status_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "cource_module_status",
                        principalColumn: "ModuleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cource_matherial_cources_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "cources",
                        principalColumn: "column_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_cource_enrollments_CourceId",
                table: "cource_enrollments",
                column: "CourceId");

            migrationBuilder.CreateIndex(
                name: "IX_cource_enrollments_EnrollmentId",
                table: "cource_enrollments",
                column: "EnrollmentId");

            migrationBuilder.CreateIndex(
                name: "IX_cource_status_EnrollmentId",
                table: "cource_status",
                column: "EnrollmentId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cource_enrollments");

            migrationBuilder.DropTable(
                name: "cource_matherial");

            migrationBuilder.DropTable(
                name: "cource_status");

            migrationBuilder.DropTable(
                name: "cource_module_status");

            migrationBuilder.DropTable(
                name: "cources");
        }
    }
}
