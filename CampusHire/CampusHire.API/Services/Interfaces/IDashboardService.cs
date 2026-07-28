using CampusHire.API.DTOs.Dashboard;

namespace CampusHire.API.Services.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardResponseDto> GetDashboardAsync();
    }
}