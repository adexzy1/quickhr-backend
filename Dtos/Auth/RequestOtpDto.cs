using System.ComponentModel.DataAnnotations;

namespace qwikhr.Dtos.Auth
{
    public class RequestOtpDto
    {
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        public OtpPurpose Purpose { get; set; }
    }
}