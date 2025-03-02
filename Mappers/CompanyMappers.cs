using qwikhr.Dtos.Company;
using qwikhr.Models;

namespace qwikhr.Mappers
{
    public static class CompanyMappers
    {
        public static CompanyDto ToCompanyDto(this Company comapyModel)
        {
            return new CompanyDto
            {
                Id = comapyModel.Id,
                Name = comapyModel.Name,
                Slug = comapyModel.Slug
            };
        }

        public static Company ToCompanyFromCompanyDto(this CreateCompanyDto companyDto)
        {
            return new Company
            {
                Name = companyDto.Name,
                Slug = Guid.NewGuid(),
            };
        }
    }
}