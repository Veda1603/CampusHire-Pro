using System.ComponentModel.DataAnnotations;

namespace CampusHire.API.Entities
{
    public class RegistrationDeadline
    {
        [Key]
        public int RegistrationDeadlineId { get; set; }

        [Required]
        public string DriveName { get; set; } = string.Empty;

        [Required]
        public DateTime RegistrationStartDate { get; set; }

        [Required]
        public DateTime RegistrationEndDate { get; set; }

        public string Status { get; set; } = "Active";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}