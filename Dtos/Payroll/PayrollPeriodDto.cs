namespace qwikhr.Dtos.Payroll
{
    public class PayrollPeriodDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty; // e.g., "April 2025"
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime PayDate { get; set; }
        public string Status { get; set; } = "Draft"; // Enum as string
        public bool IsLocked { get; set; } = false;
    }

    public class CreatePayrollPeriodDto
    {
        public required string Name { get; set; }
        public required DateTime StartDate { get; set; }
        public required DateTime EndDate { get; set; }
        public required DateTime PayDate { get; set; }
    }

    public class UpdatePayrollPeriodDto
    {
        public required string Name { get; set; }
        public required DateTime StartDate { get; set; }
        public required DateTime EndDate { get; set; }
        public required DateTime PayDate { get; set; }
        public bool IsLocked { get; set; }
    }
}