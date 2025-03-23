using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using qwikhr.Common;

namespace qwikhr.Models
{
    public class Employee : CompanyEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;

        public decimal Salary { get; set; }
        public string Department { get; set; } = string.Empty;

        public Guid? ManagerId { get; set; }
        [ForeignKey("ManagerId")]
        public Employee? Manager { get; set; }

        public ICollection<EmployeeLeaveBalance> LeaveBalances { get; set; } = [];
        public ICollection<LeaveRequest> LeaveRequests { get; set; } = [];
        public ICollection<ShiftSchedule> ShiftSchedules { get; set; } = [];
    }

}