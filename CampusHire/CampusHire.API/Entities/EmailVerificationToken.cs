using System.ComponentModel.DataAnnotations;

namespace CampusHire.API.Entities
{
    public class EmailVerificationToken
    {
        [Key]
        public int Id { get; set; }
        public int AdminId { get; set; }
        public string Token { get; set; } = string.Empty;
        public DateTime ExpiryTime { get; set; }
        public bool IsUsed { get; set; } = false;
        public Admin? Admin { get; set; }
    }
}