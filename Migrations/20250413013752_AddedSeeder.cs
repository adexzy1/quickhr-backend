using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace qwikhr.Migrations
{
    /// <inheritdoc />
    public partial class AddedSeeder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PayeTaxBands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LowerBound = table.Column<decimal>(type: "numeric(12,2)", nullable: false),
                    UpperBound = table.Column<decimal>(type: "numeric(12,2)", nullable: true),
                    Rate = table.Column<decimal>(type: "numeric(5,4)", nullable: false),
                    AnnualCumulative = table.Column<decimal>(type: "numeric(12,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayeTaxBands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PayrollStatutoryDeductions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    PayrollEntryId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    RateCode = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    EmployeeAmount = table.Column<decimal>(type: "numeric(12,2)", nullable: false),
                    EmployerAmount = table.Column<decimal>(type: "numeric(12,2)", nullable: true),
                    CalculationMetadata = table.Column<Dictionary<string, object>>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayrollStatutoryDeductions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PayrollStatutoryDeductions_PayrollEntries_PayrollEntryId",
                        column: x => x.PayrollEntryId,
                        principalTable: "PayrollEntries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StatutoryDeductions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Code = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    EmployeeRate = table.Column<decimal>(type: "numeric", nullable: false),
                    EmployerRate = table.Column<decimal>(type: "numeric", nullable: true),
                    LegalReference = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    ApplyToAllCompanies = table.Column<bool>(type: "boolean", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    EffectiveDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatutoryDeductions", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "PayeTaxBands",
                columns: new[] { "Id", "AnnualCumulative", "LowerBound", "Rate", "UpperBound" },
                values: new object[,]
                {
                    { 1, 0m, 0m, 0.07m, 300000m },
                    { 2, 21000m, 300001m, 0.11m, 600000m },
                    { 3, 54000m, 600001m, 0.15m, 1100000m },
                    { 4, 129000m, 1100001m, 0.19m, 1600000m },
                    { 5, 224000m, 1600001m, 0.21m, 3200000m },
                    { 6, 560000m, 3200001m, 0.24m, null }
                });

            migrationBuilder.InsertData(
                table: "StatutoryDeductions",
                columns: new[] { "Id", "ApplyToAllCompanies", "Code", "EffectiveDate", "EmployeeRate", "EmployerRate", "IsActive", "LegalReference", "Name" },
                values: new object[,]
                {
                    { new Guid("18e12e5d-7a5e-4920-9b85-411b5b1e0c21"), true, "PEN", new DateTime(2014, 7, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0.08m, 0.10m, true, "Pension Reform Act 2014 Section 4(1)", "Pension Contribution" },
                    { new Guid("2b8e1d5a-9c3f-4e7d-b6a1-8d5f3e2c1a7b"), true, "NSITF", new DateTime(2011, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0m, 0.01m, true, "Employee Compensation Act 2010", "NSITF Contribution" },
                    { new Guid("4a7b1e9f-2c63-4e9a-b7a6-8d3f1e5d2c8a"), true, "NHF", new DateTime(1992, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0.025m, null, true, "NHF Act 1992 Section 6", "National Housing Fund" },
                    { new Guid("5d3e8f2a-1b7c-4e9a-a8d6-7c3b8f1e5d2b"), true, "ITF", new DateTime(2011, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0m, 0.01m, true, "ITF Act 2011 Section 6(1)", "ITF Levy" },
                    { new Guid("7f3e8d2a-1b5c-4e9f-a8d7-6c3b9f1e5d2a"), true, "PAYE", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, true, "PITA 2011 as amended", "PAYE Tax" },
                    { new Guid("9a8b7c6d-5e4f-3a2b-1c0d-9e8f7a6b5c4d"), true, "NHIA", new DateTime(2022, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0.015m, 0.035m, true, "NHIA Act 2022 Section 25", "NHIA Contribution" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PayGrades_Code",
                table: "PayGrades",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PayComponents_Code",
                table: "PayComponents",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PayeTaxBands_LowerBound_UpperBound",
                table: "PayeTaxBands",
                columns: new[] { "LowerBound", "UpperBound" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PayrollStatutoryDeductions_PayrollEntryId",
                table: "PayrollStatutoryDeductions",
                column: "PayrollEntryId");

            migrationBuilder.CreateIndex(
                name: "IX_StatutoryDeductions_Code",
                table: "StatutoryDeductions",
                column: "Code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PayeTaxBands");

            migrationBuilder.DropTable(
                name: "PayrollStatutoryDeductions");

            migrationBuilder.DropTable(
                name: "StatutoryDeductions");

            migrationBuilder.DropIndex(
                name: "IX_PayGrades_Code",
                table: "PayGrades");

            migrationBuilder.DropIndex(
                name: "IX_PayComponents_Code",
                table: "PayComponents");
        }
    }
}
