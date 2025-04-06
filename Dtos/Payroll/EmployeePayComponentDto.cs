namespace qwikhr.Dtos.Payroll
{
    public class EmployeePayComponentDto
    {
        public Guid Id { get; set; } // The unique ID of the EmployeePayComponent
        public string PayComponentName { get; set; } = string.Empty; // The name of the pay component
        public decimal Amount { get; set; } // The value of the pay component
        public string Frequency { get; set; } = "Monthly"; // Frequency (e.g., Monthly, Weekly)
        public DateTime EffectiveDate { get; set; } // The effective date of the pay component
        public DateTime? EndDate { get; set; } // The optional end date of the pay component
        public bool IsActive { get; set; } // Indicates if the pay component is active
    }
}