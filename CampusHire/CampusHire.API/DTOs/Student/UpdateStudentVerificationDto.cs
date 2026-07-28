namespace CampusHire.API.DTOs.Student
{
    public class UpdateStudentVerificationDto
    {
        public string Status { get; set; } = string.Empty;
        public string? Remarks { get; set; }
        public int VerifiedByAdminId { get; set; }
        public string VerificationType { get; set; } = "Manual";
    }
}