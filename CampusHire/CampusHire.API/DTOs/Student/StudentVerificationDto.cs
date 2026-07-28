namespace CampusHire.API.DTOs.Student
{
    public class StudentVerificationDto
    {
        public int VerificationId { get; set; }
        public int StudentId { get; set; }
        public int VerifiedByAdminId { get; set; }
        public string VerificationType { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string? Remarks { get; set; }
        public DateTime VerifiedOn { get; set; }
    }
}