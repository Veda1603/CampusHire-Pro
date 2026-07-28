using CampusHire.API.DTOs.Report;

namespace CampusHire.API.Services.Interfaces
{
    public interface IReportService
    {
        Task<ReportDto> GetReportAsync();
    }
}