using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using qwikhr.Dtos.Employee;
using qwikhr.Interfaces;
using qwikhr.Mappers;
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
        private readonly IEmployeeRepository _employeeRepo;

        public EmployeeController(CreateEmployeeService createEmployeeService, IEmailService emailService, IEmployeeRepository employeeRepo)
        {
            _createEmployeeService = createEmployeeService;
            _emailService = emailService;
            _employeeRepo = employeeRepo;
        }

        [AllowAnonymous]
        [HttpPost("create")]
        public async Task<IActionResult> Register([FromBody] CreateEmployeeDto employeeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var tempPassword = GenerateSecurePassword();

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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var employees = await _employeeRepo.GetAllAsync();
            var employeeDto = employees.Select(e => e.ToEmployeeDto());
            return Ok(employeeDto);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var employee = await _employeeRepo.GetByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee.ToEmployeeDto());
        }

        private static string GenerateSecurePassword(int length = 12)
        {
            if (length < 6) length = 6; // Ensure minimum length

            const string Uppercase = "ABCDEFGHJKLMNOPQRSTUVWXYZ";
            const string Lowercase = "abcdefghijklmnopqrstuvwxyz";
            const string Digits = "0123456789";
            const string SpecialChars = "!@#$%^&*?_-";
            const string AllChars = Uppercase + Lowercase + Digits + SpecialChars;

            var random = new Random();
            var password = new StringBuilder();
            password.Append(Uppercase[random.Next(Uppercase.Length)]);
            password.Append(Lowercase[random.Next(Lowercase.Length)]);
            password.Append(Digits[random.Next(Digits.Length)]);
            password.Append(SpecialChars[random.Next(SpecialChars.Length)]);

            for (int i = 4; i < length; i++)
            {
                password.Append(AllChars[random.Next(AllChars.Length)]);
            }

            return new string(password.ToString().OrderBy(_ => random.Next()).ToArray()); // Shuffle the characters
        }

    }
}

