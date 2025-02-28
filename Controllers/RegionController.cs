using Microsoft.EntityFrameworkCore;
using qwikhr.Data;
using qwikhr.Mappers;
using qwikhr.Models;

namespace qwikhr.Controllers;

using Microsoft.AspNetCore.Mvc;

[Route("api/regions")]
[ApiController]
public class RegionController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly RegionMapper _regionMapper;

    public RegionController(ApplicationDbContext context, RegionMapper regionMapper)
    {
        _context = context;
        _regionMapper = regionMapper;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Region>>> GetRegions()
    {
        var regions = await _context.Regions.ToListAsync();
        return Ok(_regionMapper.RegionListToDtoList(regions));
    }
    
    [HttpGet("{slug}")]
    public async Task<ActionResult<Region>> GetRegion(Guid slug)
    {
        var region = await _context.Regions.FirstOrDefaultAsync(r => r.Slug == slug);

        if (region == null)
        {
            return NotFound();
        }

        return Ok(_regionMapper.RegionToDto(region));
    }
    
    [HttpPut("{slug}")]
    public async Task<IActionResult> PutRegion(Guid slug, Region region)
    {
        if (slug != region.Slug)
        {
            return BadRequest();
        }

        var existingRegion = await _context.Regions.FirstOrDefaultAsync(r => r.Slug == slug);

        if(existingRegion == null)
        {
            return NotFound();
        }

        region.Id = existingRegion.Id;
        region.UpdatedAt = DateTime.UtcNow;

        _context.Entry(existingRegion).CurrentValues.SetValues(region);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!RegionExists(slug))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }
    
    [HttpPost]
    public async Task<ActionResult<Region>> PostRegion(Region region)
    {
        region.CreatedAt = DateTime.UtcNow;
        region.UpdatedAt = DateTime.UtcNow;
        _context.Regions.Add(region);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetRegion", new { slug = region.Slug }, _regionMapper.RegionToDto(region));
    }
    
    [HttpDelete("{slug}")]
    public async Task<IActionResult> DeleteRegion(Guid slug)
    {
        var region = await _context.Regions.FirstOrDefaultAsync(r => r.Slug == slug);
        if (region == null)
        {
            return NotFound();
        }

        _context.Regions.Remove(region);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool RegionExists(Guid slug)
    {
        return _context.Regions.Any(e => e.Slug == slug);
    }
}