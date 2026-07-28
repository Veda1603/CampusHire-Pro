using System.ComponentModel.DataAnnotations;

namespace CampusHire.API.DTOs.PlacementDrive
{
    public class CreatePlacementDriveDto
    {
        [Required]
        public string CompanyName { get; set; } = string.Empty;

        [Required]
        public DateTime DriveDate { get; set; }

        [Required]
        public string Location { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        public bool IsActive { get; set; }
    }
}