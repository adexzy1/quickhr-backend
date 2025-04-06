using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using qwikhr.Data;
using qwikhr.Dtos.Employee;
using qwikhr.helper;
using qwikhr.Interfaces;
using qwikhr.Models;
using qwikhr.Models.Payroll;


namespace qwikhr.Services
{
    public class CreateEmployeeService
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<CreateEmployeeService> _logger;
        private readonly UserContextHelper _userContextHelper;

        public CreateEmployeeService(UserManager<User> userManager, ApplicationDbContext context, IEmployeeRepository employeeRepository, ILogger<CreateEmployeeService> logger, UserContextHelper userContextHelper)
        {
            _userManager = userManager;
            _context = context;
            _employeeRepository = employeeRepository;
            _logger = logger;
            _userContextHelper = userContextHelper;
        }

        public async Task<(bool IsSuccess, string Message)> CreateEmployeeAsync(CreateEmployeeDto dto, string password)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Step 1: Create the User
                var user = new User
                {
                    UserName = dto.Email,
                    Email = dto.Email,
                    PhoneNumber = dto.PhoneNumber,
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    MiddleName = dto.MiddleName,
                    CompanyId = _userContextHelper.GetUserCompanyIdOrNull() ?? Guid.Empty
                };

                var result = await _userManager.CreateAsync(user, password);
                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    _logger.LogError("User creation failed: {Errors}", errors);
                    await transaction.RollbackAsync();
                    return (false, $"User creation failed: {errors}");
                }

                var createdUser = await _userManager.FindByEmailAsync(dto.Email);
                if (createdUser == null)
                {
                    _logger.LogError("Failed to retrieve the created user: {Email}", dto.Email);
                    await transaction.RollbackAsync();
                    return (false, "Failed to retrieve the created user.");
                }
                await _userManager.AddToRoleAsync(createdUser, UserRole.Employee.ToString());


                // Step 2: Use the UserId to create the Employee
                var employee = new Employee
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    MiddleName = dto.MiddleName,
                    Email = dto.Email,
                    PhoneNumber = dto.PhoneNumber,
                    DateOfBirth = dto.DateOfBirth,
                    Gender = dto.Gender,
                    MaritalStatus = dto.MaritalStatus,
                    EmploymentDate = dto.EmploymentDate,
                    EmploymentTypeId = dto.EmploymentTypeId,
                    PositionId = dto.PositionId,
                    DepartmentId = dto.DepartmentId,
                    BankName = dto.BankName,
                    AccountNumber = dto.AccountNumber,
                    BVN = dto.BVN,
                    PensionFundAdministrator = dto.PensionFundAdministrator,
                    PensionNumber = dto.PensionNumber,
                    TaxIdentificationNumber = dto.TaxIdentificationNumber,
                    NextOfKinName = dto.NextOfKinName,
                    NextOfKinPhone = dto.NextOfKinPhone,
                    NextOfKinRelationship = dto.NextOfKinRelationship,
                    UserId = user.Id, // Link the created User
                    PayGradeId = dto.PayGradeId
                };

                var createdEmployee = await _employeeRepository.CreateAsync(employee);
                if (createdEmployee == null)
                {
                    await transaction.RollbackAsync();
                    _logger.LogError("Failed to create employee record for user: {Email}", dto.Email);
                    return (false, "Failed to create employee record.");
                }
                // Step 3: Seed Default PayComponents for the Employee
                var payGrade = await _context.PayGrades
                    .Include(pg => pg.PayGradeComponents)
                    .ThenInclude(pgc => pgc.PayComponent)
                    .FirstOrDefaultAsync(pg => pg.Id == dto.PayGradeId);

                if (payGrade == null)
                {
                    await transaction.RollbackAsync();
                    _logger.LogError("PayGrade not found for PayGradeId: {PayGradeId}", dto.PayGradeId);
                    return (false, "Invalid PayGradeId provided.");
                }

                // Create EmployeePayComponents with default values
                var employeePayComponents = payGrade.PayGradeComponents.Select(pgc => new EmployeePayComponent
                {
                    EmployeeId = createdEmployee.Id,
                    PayComponentId = pgc.PayComponentId,
                    Amount = 0, // Default value
                    Frequency = "Monthly", // Default frequency
                    EffectiveDate = DateTime.UtcNow,
                    IsActive = true
                }).ToList();
                await _context.EmployeePayComponents.AddRangeAsync(employeePayComponents);
                await _context.SaveChangesAsync();

                // Commit the transaction
                await transaction.CommitAsync();
                return (true, "Account created successfully");
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                if (e is DbUpdateException dbEx && dbEx.InnerException != null)
                {
                    _logger.LogError(dbEx.InnerException, "Database update failed: {Message}", dbEx.InnerException.Message);
                }
                else
                {
                    _logger.LogError(e, "An error occurred: {Message}", e.Message);
                }
                return (false, "An unexpected error occurred. Please try again later.");
            }

        }

    }
}
