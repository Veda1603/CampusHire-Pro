using System.ComponentModel.DataAnnotations;

namespace CampusHire.API.DTOs.Student
{
    public class CreateStudentVerificationDto
    {
        [Required]
        public int StudentId { get; set; }
        public int VerifiedByAdminId { get; set; }
        public string VerificationType { get; set; } = "Manual";
        [Required]
        public string Status { get; set; } = string.Empty;
        
        public string? Remarks { get; set; }
    }
}