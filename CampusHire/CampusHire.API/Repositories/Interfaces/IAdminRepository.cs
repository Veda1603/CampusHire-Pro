using CampusHire.API.Entities;

namespace CampusHire.API.Repositories.Interfaces
{
    public interface IAdminRepository
    {
        Task<List<Admin>> GetAllAsync();
        Task<Admin?> GetByIdAsync(int id);
        Task<Admin?> GetByEmailAsync(string email);
        Task AddAsync(Admin admin);
        Task UpdateAsync(Admin admin);
        Task DeleteAsync(Admin admin);
        Task UpdateRefreshTokenAsync(int adminId, string refreshToken, DateTime expiry);
        Task<Admin?> GetByRefreshTokenAsync(string refreshToken);
        Task<Admin?> GetByResetTokenAsync(string token);
    }
}