namespace CampusHire.API.DTOs.Eligibility;

public class EligibilityCriteriaDto
{
    public int EligibilityCriteriaId { get; set; }
    public string DriveName { get; set; } = string.Empty;
    public decimal MinimumCGPA { get; set; }
    public int MaximumBacklogs { get; set; }
    public string AllowedDepartments { get; set; } = string.Empty;
    public int PassingYear { get; set; }
}