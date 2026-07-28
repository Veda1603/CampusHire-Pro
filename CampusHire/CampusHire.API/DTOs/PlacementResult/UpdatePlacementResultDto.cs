namespace CampusHire.API.DTOs.PlacementResult
{
    public class UpdatePlacementResultDto
    {
        public string CompanyName { get; set; } = string.Empty;
        public string? JobRole { get; set; }
        public decimal Package { get; set; }
        public string Status { get; set; } = "Selected";
        public DateTime PlacementDate { get; set; }
    }
}