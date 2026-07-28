using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CampusHire.API.Entities
{
    public class PlacementResult
    {
        [Key]
        public int PlacementResultId { get; set; }
        [Required]
        public int StudentId { get; set; }
        [Required]
        public string StudentName { get; set; } = string.Empty;
        [Required]
        public string CompanyName { get; set; } = string.Empty;
        public string? JobRole { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Package { get; set; }
        [Required]
        public string Status { get; set; } = "Selected";
        public DateTime PlacementDate { get; set; } = DateTime.UtcNow;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}