using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CampusHire.API.Entities;

public class EligibilityCriteria
{
    [Key]
    public int EligibilityCriteriaId { get; set; }
    [Required]
    public string DriveName { get; set; } = string.Empty;

    [Column(TypeName = "decimal(3,2)")]
    public decimal MinimumCGPA { get; set; }
    public int MaximumBacklogs { get; set; }
    public string AllowedDepartments { get; set; } = string.Empty;
    public int PassingYear { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}