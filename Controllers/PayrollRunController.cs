using Microsoft.AspNetCore.Mvc;
using qwikhr.Services;
using qwikhr.Interfaces;
using qwikhr.Dtos.Payroll;
using qwikhr.Mappers;
using Microsoft.EntityFrameworkCore;

namespace qwikhr.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PayrollRunController : ControllerBase
    {
        private readonly PayrollService _payrollService;
        private readonly IPayrollRunRepository _repository;
        private readonly ILogger<PayrollRunController> _logger;

        public PayrollRunController(PayrollService payrollService, ILogger<PayrollRunController> logger, IPayrollRunRepository repository)
        {
            _payrollService = payrollService;
            _logger = logger;
            _repository = repository;
        }

        // Get all payroll runs
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var payrollRuns = await _repository.GetAllAsync();
                var payrollRunDtos = payrollRuns.Select(pr => pr.ToPayrollRunDto());
                return Ok(payrollRunDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching payroll runs.");
                return StatusCode(500, new { Message = "An error occurred while fetching payroll runs." });
            }
        }

        // Get a payroll run by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var payrollRun = await _repository.GetByIdAsync(id);
                if (payrollRun == null)
                    return NotFound(new { Message = "Payroll run not found." });

                return Ok(payrollRun.ToPayrollRunDto());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching payroll run with ID: {Id}", id);
                return StatusCode(500, new { Success = false, Message = "An error occurred while fetching the payroll run." });
            }
        }

        // Create a new payroll run
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePayrollRunDto payrollRun)
        {
            try
            {
                var payrollRunEntity = payrollRun.ToPayrollRunFromCreateDto();
                await _repository.AddAsync(payrollRunEntity, payrollRun.EmployeeIds);
                return CreatedAtAction(nameof(GetById), new { id = payrollRunEntity.Id }, payrollRun);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating payroll run.");
                return StatusCode(500, new { Message = "An error occurred while creating the payroll run." });
            }
        }

        // Update an existing payroll run
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePayrollRunDto payrollRun)
        {
            try
            {
                var existingPayrollRun = await _repository.UpdateAsync(id, payrollRun);
                if (existingPayrollRun == null)
                {
                    return NotFound(new { Message = "Payroll run not found." });
                }
                return Ok(existingPayrollRun.ToPayrollRunDto());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating payroll run with ID: {Id}", id);
                return StatusCode(500, new { Message = "An error occurred while updating the payroll run." });
            }
        }

        // Delete a payroll run
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var payrollRun = await _repository.DeleteAsync(id);
                if (payrollRun == null)
                {
                    return NotFound(new { Message = "Payroll run not found." });
                }

                return NoContent();
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

        // Initiate a payroll run
        [HttpPost("{payrollRunId}/initiate")]
        public async Task<IActionResult> InitiatePayrollRun(Guid payrollRunId)
        {
            try
            {
                var payrollRun = await _payrollService.RunPayrollWorkflowAsync(payrollRunId);
                return Ok(new
                {
                    Message = "Payroll run initiated successfully.",
                    PayrollRun = payrollRun
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error initiating payroll run for ID: {PayrollRunId}", payrollRunId);
                return StatusCode(500, new { Message = ex.Message });
            }
        }

        // Endpoint to process payroll entries
        [HttpPost("process/{payrollRunId}")]
        public async Task<IActionResult> ProcessPayrollAsync(Guid payrollRunId)
        {
            try
            {
                // Call the service method to process payroll entries
                var payrollRun = await _payrollService.ProcessPayrollEntriesAsync(payrollRunId);

                // Return the processed payroll run
                return Ok(new
                {
                    Message = "Payroll processed successfully.",
                    PayrollRun = payrollRun
                });
            }
            catch (Exception ex)
            {
                // Handle errors and return a bad request response
                return BadRequest(new
                {
                    Message = "An error occurred while processing the payroll.",
                    Error = ex.Message
                });
            }
        }

        // Finalize a payroll run
        [HttpPost("{payrollRunId}/finalize")]
        public async Task<IActionResult> FinalizePayrollRun(Guid payrollRunId)
        {
            try
            {
                var result = await _payrollService.FinalizePayrollRunAsync(payrollRunId);
                return Ok(new { Success = result, Message = "Payroll run finalized successfully." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error finalizing payroll run.");
                return StatusCode(500, new { Message = ex.Message });
            }
        }
    }
}