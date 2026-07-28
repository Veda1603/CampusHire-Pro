using System.ComponentModel.DataAnnotations;

namespace CampusHire.API.Entities
{
    public class PlacementDrive
    {
        [Key]
        public int DriveId { get; set; }

        [Required]
        [StringLength(150)]
        public string CompanyName { get; set; } = string.Empty;

        [Required]
        public DateTime DriveDate { get; set; }

        [Required]
        public string Location { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;
    }
}