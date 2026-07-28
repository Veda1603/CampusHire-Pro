using System.ComponentModel.DataAnnotations;

namespace CampusHire.API.Entities
{
    public class StudentVerification
    {
        [Key]
        public int VerificationId { get; set; }

        public int StudentId { get; set; }

        public int VerifiedByAdminId { get; set; }

        public string VerificationType { get; set; } = "Manual";

        public string Status { get; set; } = "Pending";

        public string? Remarks { get; set; }

        public DateTime VerifiedOn { get; set; } = DateTime.UtcNow;
    }
}