using CampusHire.API.Entities;

namespace CampusHire.API.Repositories.Interfaces
{
    public interface IAdminActivityRepository
    {
        Task<List<AdminActivityLog>> GetAllAsync();
        Task<List<AdminActivityLog>> GetByAdminIdAsync(int adminId);
        Task AddAsync(AdminActivityLog activity);
    }
}