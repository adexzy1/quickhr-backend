using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace qwikhr.Migrations
{
    /// <inheritdoc />
    public partial class AddedConcurrencyId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "ShiftSchedules",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "Regions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "Positions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "PayrollRuns",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "PayrollPeriods",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "PayrollEntryDetails",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "PayrollEntries",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "PayrollApprovals",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "PayrollApprovalHistories",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "PayGradeSteps",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "PayGrades",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "PayGradePayComponents",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "PayGradeComponents",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "PayComponents",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "Locations",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "LocationPayRules",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "LeaveTypes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "LeaveRequests",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "EmploymentTypes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "EmployeeShifts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "EmployeeSalaries",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "Employees",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "EmployeePayComponents",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "EmployeeLeaveRequests",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "EmployeeLeaveBalances",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "Departments",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "CompanyPayrollApprovalLevels",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "Branches",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "ApprovalWorkflows",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Version",
                table: "ShiftSchedules");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "Regions");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "Positions");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "PayrollRuns");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "PayrollPeriods");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "PayrollEntryDetails");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "PayrollEntries");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "PayrollApprovals");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "PayrollApprovalHistories");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "PayGradeSteps");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "PayGrades");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "PayGradePayComponents");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "PayGradeComponents");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "PayComponents");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "LocationPayRules");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "LeaveTypes");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "LeaveRequests");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "EmploymentTypes");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "EmployeeShifts");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "EmployeeSalaries");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "EmployeePayComponents");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "EmployeeLeaveRequests");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "EmployeeLeaveBalances");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "CompanyPayrollApprovalLevels");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "Branches");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "ApprovalWorkflows");
        }
    }
}
