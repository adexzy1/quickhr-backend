using System.ComponentModel.DataAnnotations;

namespace qwikhr.Dtos.Auth
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}