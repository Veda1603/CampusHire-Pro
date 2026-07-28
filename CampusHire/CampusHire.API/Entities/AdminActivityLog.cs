using System.ComponentModel.DataAnnotations;

namespace CampusHire.API.Entities
{
    public class AdminActivityLog
    {
        [Key]
        public int ActivityId { get; set; }

        [Required]
        public int AdminId { get; set; }

        [Required]
        [StringLength(100)]
        public string Action { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string Description { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}