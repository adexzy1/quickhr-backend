using System.ComponentModel.DataAnnotations;

namespace qwikhr.Dtos.Auth
{
    public class VerifyEmaillDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Length(4, 4)]
        public string Otp { get; set; }
    }
}