namespace CampusHire.API.DTOs.PlacementResult
{
    public class PlacementResultDto
    {
        public int PlacementResultId { get; set; }

        public int StudentId { get; set; }

        public string StudentName { get; set; } = string.Empty;

        public string CompanyName { get; set; } = string.Empty;

        public string? JobRole { get; set; }

        public decimal Package { get; set; }

        public string Status { get; set; } = string.Empty;

        public DateTime PlacementDate { get; set; }
    }
}