using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using qwikhr.Data;
using qwikhr.Dtos.Auth;
using qwikhr.Interfaces;
using qwikhr.Mappers;
using qwikhr.Models;
using qwikhr.Services;

namespace qwikhr.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController(UserManager<User> userManager, ApplicationDbContext context, CreateAdminAccountService createAdminService, ITokenService tokenService, SignInManager<User> signInManager) : ControllerBase
    {
        private readonly UserManager<User> _userManager = userManager;
        private readonly ApplicationDbContext _context = context;
        private readonly CreateAdminAccountService _createAdminService = createAdminService;
        private readonly ITokenService _tokenService = tokenService;
        private readonly SignInManager<User> _signInManager = signInManager;

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var (IsSuccess, adminUserDto, Message) = await _createAdminService.RegisterUserWithCompanyAsync(registerDto.Email, registerDto.Password, registerDto.CompanyName);
            if (!IsSuccess)
            {
                return BadRequest(new { message = Message });
            }

            return Ok(new { message = Message, data = adminUserDto });
        }

        [HttpPost("admin/login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _userManager.Users.Include(u => u.Company).FirstOrDefaultAsync(u => u.Email == loginDto.Email);

            if (user == null)
            {
                return Unauthorized("Account not found");
            }
            var roles = await _userManager.GetRolesAsync(user);

            if (roles?.Contains("Emloyee") == true)
            {
                return Unauthorized("UnAuthorized");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded)
            {
                return Unauthorized("Invalid Credentials");
            }

            if (roles?.Contains("Admin") == true)
            {
                var adminUserDto = new AdminUserDto
                {
                    Id = user.Id,
                    Slug = user.Slug,
                    Email = user.Email,
                    Company = user.Company?.ToCompanyDto(),
                    EmailVerified = user.EmailConfirmed,
                    Roles = [.. roles],
                    Token = _tokenService.CreateToken(user),

                };
                return Ok(new { message = "Logged in successfully", data = adminUserDto });
            }
            return Ok(new { message = "Logged in successfully", data = user });
        }
    }
}