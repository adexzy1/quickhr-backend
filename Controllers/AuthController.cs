using Microsoft.AspNetCore.Authorization;
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
    public class AuthController(UserManager<User> userManager, ApplicationDbContext context, CreateAdminAccountService createAdminService, ITokenService tokenService, IEmailService emailService, SignInManager<User> signInManager, IOtpService otpService, ILogger<AuthController> logger, JwtCookieService cookieService) : ControllerBase
    {
        private readonly UserManager<User> _userManager = userManager;
        private readonly ApplicationDbContext _context = context;
        private readonly CreateAdminAccountService _createAdminService = createAdminService;
        private readonly ITokenService _tokenService = tokenService;
        private readonly SignInManager<User> _signInManager = signInManager;
        private readonly IEmailService _emailService = emailService;
        private readonly ILogger<AuthController> _logger = logger;
        private readonly JwtCookieService _cookieService = cookieService;

        private readonly IOtpService _otpService = otpService;

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var (IsSuccess, adminUserDto, Message) = await _createAdminService.RegisterUserWithCompanyAsync(registerDto.Email, registerDto.Password, registerDto.CompanyName);
            if (!IsSuccess)
            {
                return BadRequest(new { message = Message });
            }
            if (!string.IsNullOrEmpty(adminUserDto?.Email))
            {
                var otp = await _otpService.GenerateOtp(adminUserDto.Id, OtpPurpose.EmailVerification);
                EmailMetadata emailMetadata = new(adminUserDto.Email, "Email verification code", otp);
                await _emailService.Send(emailMetadata);
            }
            return Ok(new { message = Message, data = adminUserDto });
        }

        [AllowAnonymous]
        [HttpPost("admin/login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if the request is from a browser (you can improve this check)
            bool isWebRequest = Request.Headers.UserAgent.ToString().Contains("Mozilla");

            try
            {
                // Find the user by email (case-insensitive)
                var user = await _userManager.Users
                    .Include(u => u.Company)
                    .FirstOrDefaultAsync(u => u.Email.ToLower() == loginDto.Email.ToLower());

                if (user == null)
                {
                    return Unauthorized(new { message = "Account not found." });
                }

                // Check if the user is an employee (assuming "Employee" is the correct role name)
                var roles = await _userManager.GetRolesAsync(user);
                if (roles?.Contains("Employee") == true)
                {
                    return Unauthorized(new { message = "Unauthorized access." });
                }

                // Check if the email is confirmed
                if (!await _userManager.IsEmailConfirmedAsync(user))
                {
                    var latestOtp = await _context.Otps
                        .Where(o => o.UserId == user.Id && o.ExpiryTime > DateTime.UtcNow)
                        .OrderByDescending(o => o.ExpiryTime)
                        .FirstOrDefaultAsync();

                    if (latestOtp == null)
                    {
                        // Generate and send a new OTP if no valid OTP exists
                        var otp = await _otpService.GenerateOtp(user.Id, OtpPurpose.EmailVerification);
                        EmailMetadata emailMetadata = new(loginDto.Email, "Email verification code", otp);
                        await _emailService.Send(emailMetadata);
                    }

                    return Unauthorized(new { message = "Email not verified. Please check your email and confirm it." });
                }

                // Verify the password
                var passwordCheckResult = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
                if (!passwordCheckResult.Succeeded)
                {
                    return Unauthorized(new { message = "Invalid credentials." });
                }

                var token = _tokenService.CreateToken(user);

                if (isWebRequest)
                {
                    _cookieService.SetJwtCookie(HttpContext, token);
                }

                // Prepare the response based on the user's role
                if (roles?.Contains("Admin") == true)
                {
                    var adminUserDto = new AdminUserDto
                    {
                        Id = user.Id,
                        Slug = user.Slug,
                        Email = user.Email,
                        Company = user.Company?.ToCompanyDto(),
                        EmailVerified = user.EmailConfirmed,
                        Roles = roles.ToList(),
                        Token = token
                    };

                    return Ok(new { message = "Logged in successfully.", data = adminUserDto });
                }

                // For non-admin users
                return Ok(new { message = "Logged in successfully.", data = user });
            }
            catch (Exception e)
            {
                if (e is DbUpdateException dbEx && dbEx.InnerException != null)
                {
                    _logger.LogError(dbEx.InnerException, "Database update failed: {Message}", dbEx.InnerException.Message);
                }
                else
                {
                    _logger.LogError(e, "An error occurred: {Message}", e.Message);
                }
                return StatusCode(500, new { message = "An error occurred while processing your request. Please try again later." });

            }
        }

        [AllowAnonymous]
        [HttpPost("verify-email")]
        public async Task<IActionResult> VerifyEmail([FromBody] VerifyEmaillDto verifyEmaillDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var user = await _userManager.FindByEmailAsync(verifyEmaillDto.Email);
                if (user == null)
                {
                    return NotFound(new { message = "User not found." });
                }

                if (!await _otpService.ValidateOtp(user.Id, verifyEmaillDto.Otp, OtpPurpose.EmailVerification))
                {
                    user.EmailConfirmed = true;
                    await _userManager.UpdateAsync(user);
                    return Ok("Email verified successfully.");
                }
                return UnprocessableEntity(new { message = "Invalid or expired OTP." });
            }
            catch (Exception e)
            {
                if (e is DbUpdateException dbEx && dbEx.InnerException != null)
                {
                    _logger.LogError(dbEx.InnerException, "Database update failed: {Message}", dbEx.InnerException.Message);
                }
                else
                {
                    _logger.LogError(e, "An error occurred: {Message}", e.Message);
                }
                return StatusCode(500, new { message = "An error occurred while processing your request. Please try again later." });

            }
        }
    }
}