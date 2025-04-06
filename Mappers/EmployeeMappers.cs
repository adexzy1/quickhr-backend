using qwikhr.Dtos.Employee;
using qwikhr.Models;

namespace qwikhr.Mappers
{
    public static class EmployeeMappers
    {
        public static EmployeeDto ToEmployeeDto(this Employee employeeModel)
        {
            return new EmployeeDto
            {
                Id = employeeModel.Id,
                // Personal Information
                Name = employeeModel.FirstName + " " + employeeModel.LastName,
                Email = employeeModel.Email,
                PhoneNumber = employeeModel.PhoneNumber,
                DateOfBirth = employeeModel.DateOfBirth,
                Gender = employeeModel.Gender,
                MaritalStatus = employeeModel.MaritalStatus,
                EmploymentDate = employeeModel.EmploymentDate,
                TerminationDate = employeeModel.TerminationDate,// Assuming EmploymentType has a Name property
                PayGradeName = employeeModel.PayGrade?.Name, // Assuming PayGrade has a Name property
                PositionName = employeeModel.Position?.Name, // Assuming Position has a Name property
                DepartmentName = employeeModel.Department?.Name, // Assuming Department has a Name property
                BankName = employeeModel.BankName,
                AccountNumber = employeeModel.AccountNumber,
                BVN = employeeModel.BVN,
                TaxIdentificationNumber = employeeModel.TaxIdentificationNumber,
                PensionFundAdministrator = employeeModel.PensionFundAdministrator,
                PensionNumber = employeeModel.PensionNumber,
                NextOfKinName = employeeModel.NextOfKinName,
                NextOfKinPhone = employeeModel.NextOfKinPhone,
                NextOfKinRelationship = employeeModel.NextOfKinRelationship,
                EmploymentType = employeeModel.EmploymentType.Name,
                EmploymentTypeId = employeeModel.EmploymentTypeId,
            };
        }

        public static Employee ToEmployeeFromCreateDto(this CreateEmployeeDto employeeDto)
        {
            return new Employee
            {
                FirstName = employeeDto.FirstName,
                LastName = employeeDto.LastName,
                MiddleName = employeeDto.MiddleName,
                Email = employeeDto.Email,
                PhoneNumber = employeeDto.PhoneNumber,
                DateOfBirth = employeeDto.DateOfBirth,
                Gender = employeeDto.Gender,
                MaritalStatus = employeeDto.MaritalStatus,
                EmploymentDate = employeeDto.EmploymentDate,
                BankName = employeeDto.BankName,
                AccountNumber = employeeDto.AccountNumber,
                BVN = employeeDto.BVN,
                TaxIdentificationNumber = employeeDto.TaxIdentificationNumber,
                PensionFundAdministrator = employeeDto.PensionFundAdministrator,
                PensionNumber = employeeDto.PensionNumber,
                NextOfKinName = employeeDto.NextOfKinName,
                NextOfKinPhone = employeeDto.NextOfKinPhone,
                NextOfKinRelationship = employeeDto.NextOfKinRelationship,
                PayGradeId = employeeDto.PayGradeId,
                EmploymentTypeId = employeeDto.EmploymentTypeId
            };
        }

    }
}