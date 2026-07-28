namespace CampusHire.API.DTOs.Common
{
    public class ValidationErrorResponse
    {
        public bool Success { get; set; } = false;
        public int StatusCode { get; set; } = 400;
        public string ErrorCode { get; set; } = "VALIDATION_ERROR";
        public string Message { get; set; } = "Validation failed";
        public Dictionary<string, string[]> Errors { get; set; } = new();
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}