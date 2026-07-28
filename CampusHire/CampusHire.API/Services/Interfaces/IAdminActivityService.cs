using CampusHire.API.DTOs.Admin;

namespace CampusHire.API.Services.Interfaces
{
    public interface IAdminActivityService
    {
        Task<List<AdminActivityDto>> GetAllAsync();
        Task<List<AdminActivityDto>> GetByAdminIdAsync(int adminId);
        Task AddAsync(int adminId, string action, string description);
    }
}