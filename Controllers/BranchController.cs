using Microsoft.AspNetCore.Mvc;
using qwikhr.Dtos.Branch;
using qwikhr.Interfaces;
using qwikhr.Mappers;

namespace qwikhr.Controllers
{
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
            var branchDto = branches.Select(b => b.ToBranchDto());
            return Ok(branchDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBySlug([FromRoute] Guid id)
        {
            var branch = await _branchRepo.GetByIdAsync(id);
            if (branch == null)
            {
                return NotFound();
            }
            return Ok(branch.ToSingleBranchDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBranchDto branchdto)
        {
            var branchModel = branchdto.ToBranchFromCreateDto();
            await _branchRepo.CreateAsync(branchModel);
            return CreatedAtAction(nameof(GetBySlug), new { id = branchModel.Id }, branchModel.ToBranchDto());
        }

        [HttpPut("{slug}")]
        public async Task<IActionResult> Update([FromRoute] Guid slug, [FromBody] UpdateBranchDto branchDto)
        {
            var branchModel = await _branchRepo.UpdateAsync(slug, branchDto);
            if (branchModel == null)
            {
                return NotFound();
            }
            return Ok(branchModel.ToBranchDto());
        }

        [HttpDelete("{slug}")]
        public async Task<IActionResult> Delete([FromRoute] Guid slug)
        {
            var branch = await _branchRepo.DeleteAsync(slug);
            if (branch == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
};