using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace qwikhr.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTableNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_companies_CompanyId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_branches_companies_CompanyId",
                table: "branches");

            migrationBuilder.DropForeignKey(
                name: "FK_branches_regions_RegionId",
                table: "branches");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyPayrollApprovalLevels_companies_CompanyId",
                table: "CompanyPayrollApprovalLevels");

            migrationBuilder.DropForeignKey(
                name: "FK_departments_Employees_ManagerId",
                table: "departments");

            migrationBuilder.DropForeignKey(
                name: "FK_departments_branches_BranchId",
                table: "departments");

            migrationBuilder.DropForeignKey(
                name: "FK_departments_companies_CompanyId",
                table: "departments");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeLeaveBalances_companies_CompanyId",
                table: "EmployeeLeaveBalances");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeLeaveRequests_companies_CompanyId",
                table: "EmployeeLeaveRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeePayAdjustments_companies_CompanyId",
                table: "EmployeePayAdjustments");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_companies_CompanyId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_departments_DepartmentId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_positions_PositionId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeShifts_companies_CompanyId",
                table: "EmployeeShifts");

            migrationBuilder.DropForeignKey(
                name: "FK_LeaveRequests_companies_CompanyId",
                table: "LeaveRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_LeaveTypes_companies_CompanyId",
                table: "LeaveTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_PayComponents_companies_CompanyId",
                table: "PayComponents");

            migrationBuilder.DropForeignKey(
                name: "FK_PayGrades_companies_CompanyId",
                table: "PayGrades");

            migrationBuilder.DropForeignKey(
                name: "FK_PayrollApprovalHistories_companies_CompanyId",
                table: "PayrollApprovalHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_PayrollApprovals_companies_CompanyId",
                table: "PayrollApprovals");

            migrationBuilder.DropForeignKey(
                name: "FK_positions_companies_CompanyId",
                table: "positions");

            migrationBuilder.DropForeignKey(
                name: "FK_regions_companies_CompanyId",
                table: "regions");

            migrationBuilder.DropForeignKey(
                name: "FK_ShiftSchedules_companies_CompanyId",
                table: "ShiftSchedules");

            migrationBuilder.DropTable(
                name: "PayGrdaePayComponents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_regions",
                table: "regions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_positions",
                table: "positions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_departments",
                table: "departments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_companies",
                table: "companies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_branches",
                table: "branches");

            migrationBuilder.RenameTable(
                name: "regions",
                newName: "Regions");

            migrationBuilder.RenameTable(
                name: "positions",
                newName: "Positions");

            migrationBuilder.RenameTable(
                name: "departments",
                newName: "Departments");

            migrationBuilder.RenameTable(
                name: "companies",
                newName: "Companies");

            migrationBuilder.RenameTable(
                name: "branches",
                newName: "Branches");

            migrationBuilder.RenameIndex(
                name: "IX_regions_CompanyId",
                table: "Regions",
                newName: "IX_Regions_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_positions_CompanyId",
                table: "Positions",
                newName: "IX_Positions_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_departments_ManagerId",
                table: "Departments",
                newName: "IX_Departments_ManagerId");

            migrationBuilder.RenameIndex(
                name: "IX_departments_CompanyId",
                table: "Departments",
                newName: "IX_Departments_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_departments_BranchId",
                table: "Departments",
                newName: "IX_Departments_BranchId");

            migrationBuilder.RenameIndex(
                name: "IX_branches_RegionId",
                table: "Branches",
                newName: "IX_Branches_RegionId");

            migrationBuilder.RenameIndex(
                name: "IX_branches_CompanyId",
                table: "Branches",
                newName: "IX_Branches_CompanyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Regions",
                table: "Regions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Positions",
                table: "Positions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Departments",
                table: "Departments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Companies",
                table: "Companies",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Branches",
                table: "Branches",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "PayGradePayComponents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    PayGradeId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    PayComponentId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayGradePayComponents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PayGradePayComponents_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PayGradePayComponents_PayComponents_PayComponentId",
                        column: x => x.PayComponentId,
                        principalTable: "PayComponents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PayGradePayComponents_PayGrades_PayGradeId",
                        column: x => x.PayGradeId,
                        principalTable: "PayGrades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PayGradePayComponents_CompanyId",
                table: "PayGradePayComponents",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_PayGradePayComponents_PayComponentId",
                table: "PayGradePayComponents",
                column: "PayComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_PayGradePayComponents_PayGradeId",
                table: "PayGradePayComponents",
                column: "PayGradeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Companies_CompanyId",
                table: "AspNetUsers",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Branches_Companies_CompanyId",
                table: "Branches",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Branches_Regions_RegionId",
                table: "Branches",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyPayrollApprovalLevels_Companies_CompanyId",
                table: "CompanyPayrollApprovalLevels",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Branches_BranchId",
                table: "Departments",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Companies_CompanyId",
                table: "Departments",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Employees_ManagerId",
                table: "Departments",
                column: "ManagerId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeLeaveBalances_Companies_CompanyId",
                table: "EmployeeLeaveBalances",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeLeaveRequests_Companies_CompanyId",
                table: "EmployeeLeaveRequests",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeePayAdjustments_Companies_CompanyId",
                table: "EmployeePayAdjustments",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Companies_CompanyId",
                table: "Employees",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Departments_DepartmentId",
                table: "Employees",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Positions_PositionId",
                table: "Employees",
                column: "PositionId",
                principalTable: "Positions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeShifts_Companies_CompanyId",
                table: "EmployeeShifts",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveRequests_Companies_CompanyId",
                table: "LeaveRequests",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveTypes_Companies_CompanyId",
                table: "LeaveTypes",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PayComponents_Companies_CompanyId",
                table: "PayComponents",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PayGrades_Companies_CompanyId",
                table: "PayGrades",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PayrollApprovalHistories_Companies_CompanyId",
                table: "PayrollApprovalHistories",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PayrollApprovals_Companies_CompanyId",
                table: "PayrollApprovals",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Positions_Companies_CompanyId",
                table: "Positions",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Regions_Companies_CompanyId",
                table: "Regions",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShiftSchedules_Companies_CompanyId",
                table: "ShiftSchedules",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Companies_CompanyId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Branches_Companies_CompanyId",
                table: "Branches");

            migrationBuilder.DropForeignKey(
                name: "FK_Branches_Regions_RegionId",
                table: "Branches");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyPayrollApprovalLevels_Companies_CompanyId",
                table: "CompanyPayrollApprovalLevels");

            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Branches_BranchId",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Companies_CompanyId",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Employees_ManagerId",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeLeaveBalances_Companies_CompanyId",
                table: "EmployeeLeaveBalances");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeLeaveRequests_Companies_CompanyId",
                table: "EmployeeLeaveRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeePayAdjustments_Companies_CompanyId",
                table: "EmployeePayAdjustments");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Companies_CompanyId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Departments_DepartmentId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Positions_PositionId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeShifts_Companies_CompanyId",
                table: "EmployeeShifts");

            migrationBuilder.DropForeignKey(
                name: "FK_LeaveRequests_Companies_CompanyId",
                table: "LeaveRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_LeaveTypes_Companies_CompanyId",
                table: "LeaveTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_PayComponents_Companies_CompanyId",
                table: "PayComponents");

            migrationBuilder.DropForeignKey(
                name: "FK_PayGrades_Companies_CompanyId",
                table: "PayGrades");

            migrationBuilder.DropForeignKey(
                name: "FK_PayrollApprovalHistories_Companies_CompanyId",
                table: "PayrollApprovalHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_PayrollApprovals_Companies_CompanyId",
                table: "PayrollApprovals");

            migrationBuilder.DropForeignKey(
                name: "FK_Positions_Companies_CompanyId",
                table: "Positions");

            migrationBuilder.DropForeignKey(
                name: "FK_Regions_Companies_CompanyId",
                table: "Regions");

            migrationBuilder.DropForeignKey(
                name: "FK_ShiftSchedules_Companies_CompanyId",
                table: "ShiftSchedules");

            migrationBuilder.DropTable(
                name: "PayGradePayComponents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Regions",
                table: "Regions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Positions",
                table: "Positions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Departments",
                table: "Departments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Companies",
                table: "Companies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Branches",
                table: "Branches");

            migrationBuilder.RenameTable(
                name: "Regions",
                newName: "regions");

            migrationBuilder.RenameTable(
                name: "Positions",
                newName: "positions");

            migrationBuilder.RenameTable(
                name: "Departments",
                newName: "departments");

            migrationBuilder.RenameTable(
                name: "Companies",
                newName: "companies");

            migrationBuilder.RenameTable(
                name: "Branches",
                newName: "branches");

            migrationBuilder.RenameIndex(
                name: "IX_Regions_CompanyId",
                table: "regions",
                newName: "IX_regions_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Positions_CompanyId",
                table: "positions",
                newName: "IX_positions_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Departments_ManagerId",
                table: "departments",
                newName: "IX_departments_ManagerId");

            migrationBuilder.RenameIndex(
                name: "IX_Departments_CompanyId",
                table: "departments",
                newName: "IX_departments_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Departments_BranchId",
                table: "departments",
                newName: "IX_departments_BranchId");

            migrationBuilder.RenameIndex(
                name: "IX_Branches_RegionId",
                table: "branches",
                newName: "IX_branches_RegionId");

            migrationBuilder.RenameIndex(
                name: "IX_Branches_CompanyId",
                table: "branches",
                newName: "IX_branches_CompanyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_regions",
                table: "regions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_positions",
                table: "positions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_departments",
                table: "departments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_companies",
                table: "companies",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_branches",
                table: "branches",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "PayGrdaePayComponents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    PayComponentId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    PayGradeId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayGrdaePayComponents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PayGrdaePayComponents_PayComponents_PayComponentId",
                        column: x => x.PayComponentId,
                        principalTable: "PayComponents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PayGrdaePayComponents_PayGrades_PayGradeId",
                        column: x => x.PayGradeId,
                        principalTable: "PayGrades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PayGrdaePayComponents_companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PayGrdaePayComponents_CompanyId",
                table: "PayGrdaePayComponents",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_PayGrdaePayComponents_PayComponentId",
                table: "PayGrdaePayComponents",
                column: "PayComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_PayGrdaePayComponents_PayGradeId",
                table: "PayGrdaePayComponents",
                column: "PayGradeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_companies_CompanyId",
                table: "AspNetUsers",
                column: "CompanyId",
                principalTable: "companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_branches_companies_CompanyId",
                table: "branches",
                column: "CompanyId",
                principalTable: "companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_branches_regions_RegionId",
                table: "branches",
                column: "RegionId",
                principalTable: "regions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyPayrollApprovalLevels_companies_CompanyId",
                table: "CompanyPayrollApprovalLevels",
                column: "CompanyId",
                principalTable: "companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_departments_Employees_ManagerId",
                table: "departments",
                column: "ManagerId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_departments_branches_BranchId",
                table: "departments",
                column: "BranchId",
                principalTable: "branches",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_departments_companies_CompanyId",
                table: "departments",
                column: "CompanyId",
                principalTable: "companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeLeaveBalances_companies_CompanyId",
                table: "EmployeeLeaveBalances",
                column: "CompanyId",
                principalTable: "companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeLeaveRequests_companies_CompanyId",
                table: "EmployeeLeaveRequests",
                column: "CompanyId",
                principalTable: "companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeePayAdjustments_companies_CompanyId",
                table: "EmployeePayAdjustments",
                column: "CompanyId",
                principalTable: "companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_companies_CompanyId",
                table: "Employees",
                column: "CompanyId",
                principalTable: "companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_departments_DepartmentId",
                table: "Employees",
                column: "DepartmentId",
                principalTable: "departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_positions_PositionId",
                table: "Employees",
                column: "PositionId",
                principalTable: "positions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeShifts_companies_CompanyId",
                table: "EmployeeShifts",
                column: "CompanyId",
                principalTable: "companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveRequests_companies_CompanyId",
                table: "LeaveRequests",
                column: "CompanyId",
                principalTable: "companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveTypes_companies_CompanyId",
                table: "LeaveTypes",
                column: "CompanyId",
                principalTable: "companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PayComponents_companies_CompanyId",
                table: "PayComponents",
                column: "CompanyId",
                principalTable: "companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PayGrades_companies_CompanyId",
                table: "PayGrades",
                column: "CompanyId",
                principalTable: "companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PayrollApprovalHistories_companies_CompanyId",
                table: "PayrollApprovalHistories",
                column: "CompanyId",
                principalTable: "companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PayrollApprovals_companies_CompanyId",
                table: "PayrollApprovals",
                column: "CompanyId",
                principalTable: "companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_positions_companies_CompanyId",
                table: "positions",
                column: "CompanyId",
                principalTable: "companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_regions_companies_CompanyId",
                table: "regions",
                column: "CompanyId",
                principalTable: "companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShiftSchedules_companies_CompanyId",
                table: "ShiftSchedules",
                column: "CompanyId",
                principalTable: "companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
