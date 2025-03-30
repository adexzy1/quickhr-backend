using Microsoft.AspNetCore.Mvc;
using qwikhr.Dtos.Payroll;
using qwikhr.Interfaces;
using qwikhr.Mappers;

namespace qwikhr.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PayComponentController : ControllerBase
    {
        private readonly IPayComponentRepository _payComponentRepository;

        public PayComponentController(IPayComponentRepository payComponentRepository)
        {
            _payComponentRepository = payComponentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var payComponents = await _payComponentRepository.GetAllAsync();
            var payComponentDto = payComponents.Select(pc => pc.ToPayComponentDto());
            return Ok(payComponents);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var payComponent = await _payComponentRepository.GetByIdAsync(id);
            if (payComponent == null)
            {
                return NotFound(new { Message = "PayComponent not found" });
            }
            return Ok(payComponent.ToPayComponentDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePayComponentDto payComponentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var payComponent = payComponentDto.ToPayComponentFronCreateDto();
            var createdPayComponent = await _payComponentRepository.CreateAsync(payComponent);
            return CreatedAtAction(nameof(GetById), new { id = createdPayComponent.Id }, createdPayComponent);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CreatePayComponentDto payComponentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedPayComponent = await _payComponentRepository.UpdateAsync(id, payComponentDto);
            if (updatedPayComponent == null)
            {
                return NotFound(new { Message = "PayComponent not found" });
            }

            return Ok(updatedPayComponent.ToPayComponentDto());
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var (deletedPayComponent, isInUse) = await _payComponentRepository.DeleteAsync(id);
            if (isInUse)
            {
                return BadRequest(new { Message = "PayComponent is in use and cannot be deleted" });
            }

            if (deletedPayComponent == null)
            {
                return NotFound(new { Message = "PayComponent not found" });
            }

            return NoContent();
        }
    }
}