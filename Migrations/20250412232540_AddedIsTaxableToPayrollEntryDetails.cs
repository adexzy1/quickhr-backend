using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace qwikhr.Migrations
{
    /// <inheritdoc />
    public partial class AddedIsTaxableToPayrollEntryDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsTaxable",
                table: "PayrollEntryDetails",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsTaxable",
                table: "PayrollEntryDetails");
        }
    }
}
