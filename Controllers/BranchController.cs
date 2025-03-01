using Microsoft.AspNetCore.Mvc;
using qwikhr.Dtos.Branch;
using qwikhr.Interfaces;
using qwikhr.Mappers;

namespace qwikhr.Controllers;
[Route("api/branches")]
[ApiController]
public class BranchController : ControllerBase
{
    private readonly IBranchRepository _branchRepo;

    public BranchController(IBranchRepository branchRepository)
    {
        _branchRepo = branchRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var branches = await _branchRepo.GetAllAsync();
        return Ok(branches);
    }

    [HttpGet("{slug:string}")]
    public async Task<IActionResult> GetBySlug(Guid slug)
    {
        var branch = await _branchRepo.GetBySlugAsync(slug);
        if (branch == null)
        {
            return NotFound();
        }
        return Ok(branch.ToBranchDto());
    }

    [HttpPost("{companyId:int}")]
    public async Task<IActionResult> create([FromBody] CreateBranchDto branchdto)
    {
        var branchModel = branchdto.ToBranchFromCreateDto(branchdto.CompanyId);
        await _branchRepo.CreateAsync(branchModel);
        return CreatedAtAction(nameof(GetBySlug), new { slug = branchModel.Slug });
    }


}