using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace qwikhr.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_branches_regions_RegionId",
                table: "branches");

            migrationBuilder.AddColumn<Guid>(
                name: "BranchId",
                table: "departments",
                type: "uuid",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "RegionId",
                table: "branches",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValueSql: "gen_random_uuid()");

            migrationBuilder.CreateIndex(
                name: "IX_departments_BranchId",
                table: "departments",
                column: "BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_branches_regions_RegionId",
                table: "branches",
                column: "RegionId",
                principalTable: "regions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_departments_branches_BranchId",
                table: "departments",
                column: "BranchId",
                principalTable: "branches",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_branches_regions_RegionId",
                table: "branches");

            migrationBuilder.DropForeignKey(
                name: "FK_departments_branches_BranchId",
                table: "departments");

            migrationBuilder.DropIndex(
                name: "IX_departments_BranchId",
                table: "departments");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "departments");

            migrationBuilder.AlterColumn<Guid>(
                name: "RegionId",
                table: "branches",
                type: "uuid",
                nullable: false,
                defaultValueSql: "gen_random_uuid()",
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_branches_regions_RegionId",
                table: "branches",
                column: "RegionId",
                principalTable: "regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
