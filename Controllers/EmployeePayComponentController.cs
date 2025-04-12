using Microsoft.AspNetCore.Mvc;
using qwikhr.Dtos.Payroll;
using qwikhr.Interfaces;
using qwikhr.Mappers;

namespace qwikhr.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeePayComponentController : ControllerBase
    {
        private readonly IEmployeePayComponentRepository _repository;

        public EmployeePayComponentController(IEmployeePayComponentRepository repository)
        {
            _repository = repository;
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEmployeePayComponent([FromBody] UpdateEmployeePayComponentDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employeePayComponent = await _repository.GetByIdAsync(dto.Id);
            if (employeePayComponent == null)
            {
                return NotFound(new { Message = "Employee Pay Component not found" });
            }
            // Update the EmployeePayComponent using the mapper
            employeePayComponent.UpdateFromEmployeePayComponentDto(dto);

            var UpdatedComponent = await _repository.UpdateAsync(employeePayComponent);
            if (UpdatedComponent == null)
            {
                return StatusCode(422, new { Message = "Failed to update Employee Pay Component" });
            }
            return Ok(UpdatedComponent.ToEmployeePayComponentDto());
        }

        [HttpPut("batch")]
        public async Task<IActionResult> UpdateEmployeePayComponents([FromBody] List<UpdateEmployeePayComponentDto> dtos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var failedUpdates = new List<Guid>();
            foreach (var dto in dtos)
            {
                var employeePayComponent = await _repository.GetByIdAsync(dto.Id);
                if (employeePayComponent == null)
                {
                    failedUpdates.Add(dto.Id);
                    continue;
                }

                // Update the EmployeePayComponent using the mapper
                employeePayComponent.UpdateFromEmployeePayComponentDto(dto);

                var employeePayComponentModel = await _repository.UpdateAsync(employeePayComponent);
                if (employeePayComponentModel == null)
                {
                    failedUpdates.Add(dto.Id);
                    continue;
                }
            }

            if (failedUpdates.Count != 0)
            {
                return StatusCode(422, new
                {
                    Message = "Failed to update some Employee Pay Components.",
                    FailedIds = failedUpdates
                });
            }

            return Ok(new { Message = "All Employee Pay Components updated successfully." });
        }
    }
}