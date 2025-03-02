using Microsoft.AspNetCore.Identity;
using qwikhr.Data;
using qwikhr.Dtos.Auth;
using qwikhr.Dtos.Company;
using qwikhr.Interfaces;
using qwikhr.Mappers;
using qwikhr.Models;

namespace qwikhr.Services
{
    public class CreateAdminAccountService(UserManager<User> userManager, ICompanyRepository companyRepository, ApplicationDbContext context, ITokenService tokenService)
    {
        private readonly UserManager<User> _userManager = userManager;
        private readonly ICompanyRepository _companyRepository = companyRepository;
        private readonly ApplicationDbContext _context = context;
        private readonly ITokenService _tokenService = tokenService;

        public async Task<(bool IsSuccess, AdminUserDto? adminUserDto, string Message)> RegisterUserWithCompanyAsync(string email, string password, string companyName)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var company = new Company
                {
                    Name = companyName,
                    Slug = Guid.NewGuid()
                };

                var createdCompany = await _companyRepository.CreateAsync(company);


                if (createdCompany == null)
                {
                    return (false, null, "Error Creating user Account");
                }

                var user = new User
                {
                    UserName = email.ToLower(),
                    Email = email,
                    CompanyId = createdCompany.Id,
                    Status = true
                };

                var result = await _userManager.CreateAsync(user, password);
                var createdUser = await _userManager.FindByEmailAsync(email);
                if (createdUser == null)
                {
                    return (false, null, "Failed to retrieve the created user.");
                }
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(createdUser, UserRole.Admin.ToString());
                }

                var roles = await _userManager.GetRolesAsync(createdUser);


                await transaction.CommitAsync();
                return (true,
                    new AdminUserDto
                    {
                        Slug = createdUser.Slug,
                        Id = createdUser.Id,
                        Email = createdUser.Email,
                        Company = createdUser.Company?.ToCompanyDto() ?? new CompanyDto(),
                        EmailVerified = user.EmailConfirmed,
                        Roles = [.. roles],
                        Token = _tokenService.CreateToken(createdUser)
                    }
                    ,
                    "Account Created Successfully"
                );
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                return (false, null, e.Message);
            }
        }
    }
}
