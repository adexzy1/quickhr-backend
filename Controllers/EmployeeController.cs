using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using qwikhr.Dtos.Employee;
using qwikhr.Interfaces;
using qwikhr.Models;
using qwikhr.Services;

namespace qwikhr.Controllers
{
    [Route("api/employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly CreateEmployeeService _createEmployeeService;
        private readonly IEmailService _emailService;

        public EmployeeController(CreateEmployeeService createEmployeeService, IEmailService emailService)
        {
            _createEmployeeService = createEmployeeService;
            _emailService = emailService;
        }

        [AllowAnonymous]
        [HttpPost("create")]
        public async Task<IActionResult> Register([FromBody] CreateEmployeeDto employeeDto)
        {
            var tempPassword = GenerateTemporaryPassword();

            var (IsSuccess, Message) = await _createEmployeeService.CreateEmployeeAsync(employeeDto, tempPassword);
            if (!IsSuccess)
            {
                return BadRequest(new { message = Message });
            }
            if (!string.IsNullOrEmpty(employeeDto.Email))
            {
                EmailMetadata emailMetadata = new(employeeDto.Email, "Your Temporary password", tempPassword);
                await _emailService.Send(emailMetadata);
            }
            return Ok(new { message = Message });
        }

        private static string GenerateTemporaryPassword()
        {
            const string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*?_-";
            var random = new Random();
            var tempPassword = new StringBuilder();

            for (int i = 0; i < 12; i++) // 12-character password
            {
                tempPassword.Append(validChars[random.Next(validChars.Length)]);
            }

            return tempPassword.ToString();
        }

    }
}

