using Microsoft.AspNetCore.Mvc;
using qwikhr.Dtos.Payroll;
using qwikhr.Interfaces;
using qwikhr.Mappers;

namespace qwikhr.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PayGradeController : ControllerBase
    {
        private readonly IPayGradeRepository _payGradeRepository;

        public PayGradeController(IPayGradeRepository payGradeRepository)
        {
            _payGradeRepository = payGradeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var payGrades = await _payGradeRepository.GetAllAsync();
            var payGradesDto = payGrades.Select(pg => pg.ToPayGradeDto());
            return Ok(payGradesDto);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var payGrade = await _payGradeRepository.GetByIdAsync(id);
            if (payGrade == null)
            {
                return NotFound(new { Message = "PayGrade not found" });
            }
            return Ok(payGrade.ToPayGradeDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePayGradeDto payGradeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var payGrade = payGradeDto.ToPayGradeFromCreateDto();
            var createdPayGrade = await _payGradeRepository.CreateAsync(payGrade);
            var payGradeDtoResponse = createdPayGrade.ToPayGradeDto();
            return CreatedAtAction(nameof(GetById), new { id = payGradeDtoResponse.Id }, payGradeDtoResponse);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePayGradeDto payGradeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedPayGrade = await _payGradeRepository.UpdateAsync(id, payGradeDto);
            if (updatedPayGrade == null)
            {
                return NotFound(new { Message = "PayGrade not found" });
            }

            return Ok(updatedPayGrade.ToPayGradeDto());
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deletedPayGrade = await _payGradeRepository.DeleteAsync(id);
            if (deletedPayGrade == null)
            {
                return NotFound(new { Message = "PayGrade not found" });
            }

            return NoContent();
        }
    }
}