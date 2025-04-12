using qwikhr.Dtos.Employee;
using qwikhr.Dtos.Generic;
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
                TerminationDate = employeeModel.TerminationDate,
                BankName = employeeModel.BankName,
                AccountNumber = employeeModel.AccountNumber,
                BVN = employeeModel.BVN,
                TaxIdentificationNumber = employeeModel.TaxIdentificationNumber,
                PensionFundAdministrator = employeeModel.PensionFundAdministrator,
                PensionNumber = employeeModel.PensionNumber,
                NextOfKinName = employeeModel.NextOfKinName,
                NextOfKinPhone = employeeModel.NextOfKinPhone,
                NextOfKinRelationship = employeeModel.NextOfKinRelationship,
                EmploymentType = new EntityDto
                {
                    Id = employeeModel.EmploymentTypeId,
                    Name = employeeModel.EmploymentType?.Name ?? string.Empty
                },
                // Map Department
                Department = new EntityDto
                {
                    Id = employeeModel.DepartmentId,
                    Name = employeeModel.Department?.Name ?? string.Empty
                },

                // Map Position
                Position = new EntityDto
                {
                    Id = employeeModel.PositionId,
                    Name = employeeModel.Position?.Name ?? string.Empty
                },

                // Map PayGrade
                PayGrade = new EntityDto
                {
                    Id = employeeModel.PayGradeId,
                    Name = employeeModel.PayGrade?.Name ?? string.Empty
                },
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

        public static SingleEmployeeDto ToSingleEmployeeDto(this Employee employeeModel)
        {
            return new SingleEmployeeDto
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
                TerminationDate = employeeModel.TerminationDate,
                BankName = employeeModel.BankName,
                AccountNumber = employeeModel.AccountNumber,
                BVN = employeeModel.BVN,
                TaxIdentificationNumber = employeeModel.TaxIdentificationNumber,
                PensionFundAdministrator = employeeModel.PensionFundAdministrator,
                PensionNumber = employeeModel.PensionNumber,
                NextOfKinName = employeeModel.NextOfKinName,
                NextOfKinPhone = employeeModel.NextOfKinPhone,
                NextOfKinRelationship = employeeModel.NextOfKinRelationship,
                EmploymentType = new EntityDto
                {
                    Id = employeeModel.EmploymentTypeId,
                    Name = employeeModel.EmploymentType?.Name ?? string.Empty
                },
                // Map Department
                Department = new EntityDto
                {
                    Id = employeeModel.DepartmentId,
                    Name = employeeModel.Department?.Name ?? string.Empty
                },

                // Map Position
                Position = new EntityDto
                {
                    Id = employeeModel.PositionId,
                    Name = employeeModel.Position?.Name ?? string.Empty
                },

                // Map PayGrade
                PayGrade = new EntityDto
                {
                    Id = employeeModel.PayGradeId,
                    Name = employeeModel.PayGrade?.Name ?? string.Empty
                },
                // Map EmployeePayComponents
                EmployeePayComponents = [.. employeeModel.PayComponents.Select(epc => epc.ToEmployeePayComponentDto())]
            };
        }

    }
}