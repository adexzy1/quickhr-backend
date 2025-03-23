using qwikhr.Dtos.Branch;
using qwikhr.Models;

namespace qwikhr.Mappers
{
    public static class BranchMappers
    {
        public static BranchDto ToBranchDto(this Branch branchModel)
        {
            return new BranchDto
            {
                Id = branchModel.Id,
                CompanyId = branchModel.CompanyId,
                Name = branchModel.Name
            };
        }

        public static Branch ToBranchFromCreateDto(this CreateBranchDto branchDto, Guid companyId)
        {
            return new Branch
            {
                Name = branchDto.Name,
                CompanyId = companyId,
            };
        }
    }
}