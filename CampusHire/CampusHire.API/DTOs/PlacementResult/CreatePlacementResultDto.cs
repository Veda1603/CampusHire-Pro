using System.ComponentModel.DataAnnotations;

namespace CampusHire.API.DTOs.PlacementResult
{
    public class CreatePlacementResultDto
    {
        [Required]
        public int StudentId { get; set; }

        [Required]
        public string StudentName { get; set; } = string.Empty;

        [Required]
        public string CompanyName { get; set; } = string.Empty;

        public string? JobRole { get; set; }

        public decimal Package { get; set; }

        public string Status { get; set; } = "Selected";

        public DateTime PlacementDate { get; set; } = DateTime.UtcNow;
    }
}