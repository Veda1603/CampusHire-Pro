using System.ComponentModel.DataAnnotations;
namespace CampusHire.API.Entities;

public class VerificationHistory
{
    [Key]
    public int VerificationHistoryId { get; set; }
    [Required]
    public string StudentName { get; set; } = string.Empty;
    [Required]
    public string VerifiedBy { get; set; } = string.Empty;
    [Required]
    public string Status { get; set; } = string.Empty;
    public string? Remarks { get; set; }
    public DateTime VerifiedAt { get; set; } = DateTime.UtcNow;
}