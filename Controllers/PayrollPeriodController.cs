using Microsoft.AspNetCore.Mvc;
using qwikhr.Dtos.Payroll;
using qwikhr.Interfaces;
using qwikhr.Mappers;

namespace qwikhr.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PayrollPeriodController : ControllerBase
    {
        private readonly IPayrollPeriodRepository _repository;
        private readonly ILogger<PayrollPeriodController> _logger;

        public PayrollPeriodController(IPayrollPeriodRepository repository, ILogger<PayrollPeriodController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        // Get all payroll periods
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var payrollPeriods = await _repository.GetAllAsync();
                var payrollPeriodDtos = payrollPeriods.Select(p => p.ToPayrollPeriodDto());
                return Ok(payrollPeriodDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching payroll periods.");
                return StatusCode(500, new { Message = "An error occurred while fetching payroll periods." });
            }
        }

        // Get a payroll period by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            try
            {
                var payrollPeriod = await _repository.GetByIdAsync(id);
                if (payrollPeriod == null)
                    return NotFound(new { Message = "Payroll period not found." });

                var payrollPeriodDto = payrollPeriod.ToPayrollPeriodDto();
                return Ok(payrollPeriodDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching payroll period with ID: {Id}", id);
                return StatusCode(500, new { Message = "An error occurred while fetching the payroll period." });
            }
        }

        // Create a new payroll period
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePayrollPeriodDto dto)
        {
            try
            {
                var payrollPeriod = dto.ToPayrollPeriodFromCreateDto();
                await _repository.AddAsync(payrollPeriod);
                return CreatedAtAction(nameof(GetById), new { id = payrollPeriod.Id }, payrollPeriod.ToPayrollPeriodDto());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating payroll period.");
                return StatusCode(500, new { Message = "An error occurred while creating the payroll period." });
            }
        }

        // Update an existing payroll period
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdatePayrollPeriodDto dto)
        {
            try
            {
                var payrollPeriod = await _repository.UpdateAsync(id, dto);
                if (payrollPeriod == null)
                {
                    return NotFound(new { Message = "Payroll period not found." });
                }
                return Ok(payrollPeriod.ToPayrollPeriodDto());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating payroll period with ID: {Id}", id);
                return StatusCode(500, new { Success = false, Message = "An error occurred while updating the payroll period." });
            }
        }

        // Delete a payroll period
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            try
            {
                var payrollPeriod = await _repository.DeleteAsync(id);
                if (payrollPeriod == null)
                {
                    return NotFound(new { Success = false, Message = "Payroll period not found." });
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting payroll period with ID: {Id}", id);
                return StatusCode(500, new { Success = false, Message = "An error occurred while deleting the payroll period." });
            }
        }
    }
}