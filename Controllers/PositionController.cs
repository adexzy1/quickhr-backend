using Microsoft.AspNetCore.Mvc;
using qwikhr.Dtos.Position;
using qwikhr.Interfaces;
using qwikhr.Mappers;
using qwikhr.Models;

namespace qwikhr.Controllers;
[Route("api/positions")]
[ApiController]
public class PositionController : ControllerBase
{
    private readonly IPositionRepository _positionRepo;

    public PositionController(IPositionRepository positionRepository)
    {
        _positionRepo = positionRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var positions = await _positionRepo.GetAllAsync();
        return Ok(positions.Select(p => p.ToPositionDto()));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var position = await _positionRepo.GetByIdAsync(id);
        if (position == null) return NotFound();
        return Ok(position.ToPositionDto());
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePositionDto positionDto)
    {
        var position = positionDto.ToPositionFromCreateDto();
        await _positionRepo.CreateAsync(position);
        return CreatedAtAction(nameof(GetById), new { id = position.Id }, position.ToPositionDto());
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, UpdatePositionDto positionDto)
    {
        var position = await _positionRepo.GetByIdAsync(id);
        if (position == null) return NotFound();

        position.Name = positionDto.Name;
        var positionModel = await _positionRepo.UpdateAsync(id, position);
        return Ok(new Position { Id = positionModel.Id, Name = positionModel.Name });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var position = await _positionRepo.DeleteAsync(id);
        if (position == null)
        {
            return NotFound();
        }
        return NoContent();
    }
}