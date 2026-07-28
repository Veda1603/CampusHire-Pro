using System.ComponentModel.DataAnnotations;
namespace CampusHire.API.DTOs.Eligibility;

public class CreateEligibilityCriteriaDto
{
    [Required]
    public string DriveName { get; set; } = string.Empty;
    [Range(0, 10)]
    public decimal MinimumCGPA { get; set; }
    public int MaximumBacklogs { get; set; }
    public string AllowedDepartments { get; set; } = string.Empty;
    public int PassingYear { get; set; }
}