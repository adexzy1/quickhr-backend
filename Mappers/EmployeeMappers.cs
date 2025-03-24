using qwikhr.Dtos.Employee;
using qwikhr.Models;

namespace qwikhr.Mappers
{
    public static class EmployeeMappers
    {
        public static Employee MapToEmployee(CreateEmployeeDto dto)
        {
            return new Employee
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                MiddleName = dto.MiddleName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                PositionId = dto.PositionId,
                DepartmentId = dto.DepartmentId,
                PayGradeId = dto.PayGradeId,
                EmploymentDate = dto.EmploymentDate,
                EmploymentType = dto.EmploymentType,
                BankName = dto.BankName,
                AccountNumber = dto.AccountNumber,
                TaxIdentificationNumber = dto.TaxIdentificationNumber,
                PensionFundAdministrator = dto.PensionFundAdministrator,
                PensionNumber = dto.PensionNumber,
                NextOfKinName = dto.NextOfKinName,
                NextOfKinPhone = dto.NextOfKinPhone,
                NextOfKinRelationship = dto.NextOfKinRelationship,
                LeaveBalances = [],
                LeaveRequests = [],

            };
        }

    }
}