namespace CampusHire.API.DTOs.PlacementDrive
{
    public class PlacementDriveDto
    {
        public int DriveId { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public DateTime DriveDate { get; set; }
        public string Location { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}