using Microsoft.AspNetCore.Mvc;
using qwikhr.Dtos.Department;
using qwikhr.Interfaces;
using qwikhr.Mappers;

namespace qwikhr.Controllers;
[Route("api/departments")]
[ApiController]
public class DepartmentController : ControllerBase
{
    private readonly IDepartmentRepository _departmentRepo;

    public DepartmentController(IDepartmentRepository departmentRepository)
    {
        _departmentRepo = departmentRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var departments = await _departmentRepo.GetAllAsync();
        var departmentDto = departments.Select(d => d.ToDepartmentDto());
        return Ok(departmentDto);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBySlug([FromRoute] Guid id)
    {
        var department = await _departmentRepo.GetByIdAsync(id);
        if (department == null)
        {
            return NotFound();
        }
        return Ok(department.ToDepartmentDto());
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateDepartmentDto departmentDto)
    {
        var departmenthModel = departmentDto.ToDepartmentFromCreateDto();
        await _departmentRepo.CreateAsync(departmenthModel);
        return CreatedAtAction(nameof(GetBySlug), new { id = departmenthModel.Id }, departmenthModel.ToDepartmentDto());
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateDepartmentDto branchDto)
    {
        var departmentModel = await _departmentRepo.UpdateAsync(id, branchDto);
        if (departmentModel == null)
        {
            return NotFound();
        }
        return Ok(departmentModel.ToDepartmentDto());
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var department = await _departmentRepo.DeleteAsync(id);
        if (department == null)
        {
            return NotFound();
        }
        return NoContent();
    }
}