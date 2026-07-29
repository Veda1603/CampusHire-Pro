using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CampusHire.API.Entities
{
    public class LoginOtp
    {
        [Key]
        public int Id { get; set; }

        public int AdminId { get; set; }

        [ForeignKey(nameof(AdminId))]
        public Admin? Admin { get; set; }

        [Required]
        public string OtpCode { get; set; } = string.Empty;

        public DateTime ExpiryTime { get; set; }

        public bool IsUsed { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}