using Microsoft.AspNetCore.Mvc;
using qwikhr.Dtos.Company;
using qwikhr.Interfaces;
using qwikhr.Mappers;

namespace qwikhr.Controllers
{
    [Route("api/companies")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyRepository _companyRepo;

        public CompanyController(ICompanyRepository companyRepository)
        {
            _companyRepo = companyRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var companies = await _companyRepo.GetAllAsync();
            var companyDto = companies.Select(c => c.ToCompanyDto());
            return Ok(companyDto);
        }

        [HttpGet("{slug}")]
        public async Task<IActionResult> GetBySlug([FromRoute] Guid id)
        {
            var company = await _companyRepo.GetByIdAsync(id);
            if (company == null)
            {
                return NotFound();
            }
            return Ok(company.ToCompanyDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCompanyDto companyDto)
        {
            var companyModel = companyDto.ToCompanyFromCompanyDto();
            var createdItem = await _companyRepo.CreateAsync(companyModel);
            return CreatedAtAction(nameof(GetBySlug), new { id = companyModel.Id }, createdItem);
        }

        [HttpPut("{slug}")]
        public async Task<IActionResult> Update([FromRoute] Guid slug, [FromBody] UpdateCompanyDto companyDto)
        {
            var companyModel = await _companyRepo.UpdateAsync(slug, companyDto);
            if (companyModel == null)
            {
                return NotFound();
            }
            return Ok(companyModel.ToCompanyDto());
        }

        [HttpDelete("{slug}")]
        public async Task<IActionResult> Delete([FromRoute] Guid slug)
        {
            var companyModel = await _companyRepo.DeleteAsync(slug);
            if (companyModel == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}