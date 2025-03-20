using System.ComponentModel.DataAnnotations;

namespace qwikhr.Dtos.Auth
{
    public class VerifyOtpDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Length(4, 4)]
        public string Otp { get; set; }
        [Required]
        public OtpPurpose Purpose { get; set; }
    }
}