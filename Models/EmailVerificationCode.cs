using System.ComponentModel.DataAnnotations;

namespace SASTE.Models
{
    public class EmailVerificationCode
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = "";

        [Required]
        [MaxLength(6)]
        public string Code { get; set; } = "";

        public DateTime ExpiresAt { get; set; }

        public bool Used { get; set; } = false;
    }
}