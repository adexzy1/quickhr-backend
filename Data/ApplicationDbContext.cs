using qwikhr.Models;
namespace qwikhr.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using qwikhr.Extensions;
using qwikhr.helper;
using qwikhr.Seeder;

public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    private readonly UserContextHelper _userContextHelper;
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, UserContextHelper userContextHelper) : base(options)
    {
        _userContextHelper = userContextHelper;
    }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<Branch> Branches { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Region> Regions { get; set; }
    public DbSet<Position> Positions { get; set; }
    public DbSet<Otp> Otps { get; set; }
    public DbSet<ShiftSchedule> ShiftSchedules { get; set; }
    public DbSet<PayrollApproval> PayrollApprovals { get; set; }
    public DbSet<PayrollApprovalHistory> PayrollApprovalHistories { get; set; }
    public DbSet<LeaveType> LeaveTypes { get; set; }
    public DbSet<LeaveRequest> LeaveRequests { get; set; }
    public DbSet<EmployeeLeaveBalance> EmployeeLeaveBalances { get; set; }
    public DbSet<CompanyPayrollApprovalLevel> CompanyPayrollApprovalLevels { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Apply default properties
        builder.ConfigureDefaultProperties();

        // Seed default roles
        builder.Entity<IdentityRole<int>>().HasData(RoleSeeder.GetRoles());
        // otp relationship
        builder.Entity<Otp>().HasOne(o => o.User).WithMany().HasForeignKey(o => o.UserId).OnDelete(DeleteBehavior.Cascade);

        // adding global filters
        builder.Entity<Region>().HasQueryFilter(e => e.CompanyId == _userContextHelper.GetUserCompanyIdOrNull());
        builder.Entity<EmployeeLeaveBalance>().HasQueryFilter(e => e.CompanyId == _userContextHelper.GetUserCompanyIdOrNull());
        builder.Entity<LeaveRequest>().HasQueryFilter(e => e.CompanyId == _userContextHelper.GetUserCompanyIdOrNull());
        builder.Entity<LeaveType>().HasQueryFilter(e => e.CompanyId == _userContextHelper.GetUserCompanyIdOrNull());
        builder.Entity<PayrollApprovalHistory>().HasQueryFilter(e => e.CompanyId == _userContextHelper.GetUserCompanyIdOrNull());
        builder.Entity<PayrollApproval>().HasQueryFilter(e => e.CompanyId == _userContextHelper.GetUserCompanyIdOrNull());
        builder.Entity<Position>().HasQueryFilter(e => e.CompanyId == _userContextHelper.GetUserCompanyIdOrNull());
        builder.Entity<Department>().HasQueryFilter(e => e.CompanyId == _userContextHelper.GetUserCompanyIdOrNull());
        builder.Entity<Company>().HasQueryFilter(e => e.Id == _userContextHelper.GetUserCompanyIdOrNull());
        builder.Entity<Branch>().HasQueryFilter(e => e.Id == _userContextHelper.GetUserCompanyIdOrNull());
        builder.Entity<Employee>().HasQueryFilter(e => e.Id == _userContextHelper.GetUserCompanyIdOrNull());
    }


}