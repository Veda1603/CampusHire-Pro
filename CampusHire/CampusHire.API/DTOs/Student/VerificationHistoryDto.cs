namespace CampusHire.API.DTOs.Student;

public class VerificationHistoryDto
{
    public int VerificationHistoryId { get; set; }
    public string StudentName { get; set; } = string.Empty;
    public string VerifiedBy { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string? Remarks { get; set; }
    public DateTime VerifiedAt { get; set; }
}