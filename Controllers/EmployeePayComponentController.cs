using Microsoft.AspNetCore.Mvc;
using qwikhr.Dtos.Payroll;
using qwikhr.Mappers;
using qwikhr.Repository;

namespace qwikhr.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeePayComponentController : ControllerBase
    {
        private readonly EmployeePayComponentRepository _repository;

        public EmployeePayComponentController(EmployeePayComponentRepository repository)
        {
            _repository = repository;
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployeePayComponent([FromRoute] Guid id, [FromBody] UpdateEmployeePayComponentDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employeePayComponent = await _repository.GetByIdAsync(id);
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
    }
}