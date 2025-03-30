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
                Name = branchModel.Name,
                RegionId = branchModel.RegionId ?? Guid.Empty,
            };
        }

        public static Branch ToBranchFromCreateDto(this CreateBranchDto branchDto)
        {
            return new Branch
            {
                Name = branchDto.Name,
                RegionId = branchDto.RegionId
            };
        }

        public static SingleBranchDto ToSingleBranchDto(this Branch branchModel)
        {
            return new SingleBranchDto
            {
                Id = branchModel.Id,
                Name = branchModel.Name,
                RegionId = branchModel.RegionId ?? Guid.Empty,
                Departments = [.. branchModel.Departments.Select(c => c.ToDepartmentDto())]
            };
        }
    }
}