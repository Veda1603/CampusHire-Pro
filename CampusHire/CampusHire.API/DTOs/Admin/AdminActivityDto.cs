namespace CampusHire.API.DTOs.Admin
{
    public class AdminActivityDto
    {
        public int ActivityId { get; set; }

        public int AdminId { get; set; }

        public string Action { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
    }
}