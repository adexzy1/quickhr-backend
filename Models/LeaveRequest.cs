using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using qwikhr.Common;

namespace qwikhr.Models
{
    public class LeaveRequest : CompanyEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public required Employee Employee { get; set; }

        public Guid LeaveTypeId { get; set; }
        [ForeignKey("LeaveTypeId")]
        public LeaveType? LeaveType { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // Auto-calculated leave days excluding weekends
        public int TotalDays => CalculateBusinessDays(StartDate, EndDate);

        public string Status { get; set; } = "Pending"; // Pending, Approved, Rejected
        public string? Comment { get; set; }

        public Guid ApprovedById { get; set; }

        [ForeignKey("ApprovedById")]
        public Employee? ApprovedBy { get; set; }

        private static int CalculateBusinessDays(DateTime start, DateTime end)
        {
            int count = 0;
            for (var date = start; date <= end; date = date.AddDays(1))
            {
                if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
                    count++;
            }
            return count;
        }
    }

}