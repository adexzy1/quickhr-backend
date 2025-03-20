using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace qwikhr.Models
{
    public class Otp
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required int UserId { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }

        [Required]
        public required string Code { get; set; }

        public DateTime ExpiryTime { get; set; }

        [Required]
        public OtpPurpose Purpose { get; set; }

        public bool IsUsed { get; set; } = false;
    }
}