using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using qwikhr.Dtos.Auth;
using qwikhr.Interfaces;
using qwikhr.Models;

namespace qwikhr.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class OtpController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IOtpService _otpService;
        private readonly IEmailService _emailService;

        private readonly ILogger<OtpController> _logger;

        public OtpController(UserManager<User> userManager, IOtpService otpService, IEmailService emailService, ILogger<OtpController> logger)
        {
            _userManager = userManager;
            _otpService = otpService;
            _emailService = emailService;
            _logger = logger;

        }

        [HttpPost("request")]
        public async Task<IActionResult> RequestOtp([FromBody] RequestOtpDto requestDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var user = await _userManager.FindByEmailAsync(requestDto.Email);
                if (user == null)
                {
                    return NotFound("User not found.");
                }

                var otp = await _otpService.GenerateOtp(user.Id, requestDto.Purpose);

                EmailMetadata emailMetadata = new(requestDto.Email, "Email verification code", otp);
                await _emailService.Send(emailMetadata);
                return Ok(new { message = "OTP sent to your email." });
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

        [HttpPost("verify")]
        public async Task<IActionResult> VerifyOtp([FromBody] VerifyOtpDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user == null)
                {
                    return NotFound(new { message = "User not found." });
                }

                if (!await _otpService.ValidateOtp(user.Id, request.Otp, request.Purpose))
                {
                    return Ok(new { message = "OTP code verified" });
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



