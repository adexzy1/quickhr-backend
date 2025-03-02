using System.ComponentModel.DataAnnotations;

namespace qwikhr.Dtos.Auth
{
    public class RegisterDto
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }

        [Required]
        public string? CompanyName { get; set; }
    }
}