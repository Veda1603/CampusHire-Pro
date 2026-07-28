namespace CampusHire.API.DTOs.Report
{
    public class ReportFilterDto
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string? CompanyName { get; set; }
        public bool? IsActive { get; set; }
    }
}