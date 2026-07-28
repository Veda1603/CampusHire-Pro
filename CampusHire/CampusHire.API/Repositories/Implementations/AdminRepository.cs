using CampusHire.API.Data;
using CampusHire.API.Entities;
using CampusHire.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CampusHire.API.Repositories.Implementations
{
    public class AdminRepository : IAdminRepository
    {
        private readonly CampusHireDbContext _context;

        public AdminRepository(CampusHireDbContext context)
        {
            _context = context;
        }

        public async Task<List<Admin>> GetAllAsync()
        {
            return await _context.Admins.ToListAsync();
        }

        public async Task<Admin?> GetByIdAsync(int id)
        {
            return await _context.Admins.FirstOrDefaultAsync(a => a.AdminId == id);
        }

        public async Task<Admin?> GetByEmailAsync(string email)
        {
            return await _context.Admins.FirstOrDefaultAsync(a => a.Email == email);
        }
        public async Task<Admin?> GetByResetTokenAsync(string token)
        {
            return await _context.Admins.FirstOrDefaultAsync(x => x.ResetPasswordToken == token);
        }
        public async Task AddAsync(Admin admin)
        {
            await _context.Admins.AddAsync(admin);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Admin admin)
        {
            _context.Admins.Update(admin);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Admin admin)
        {
            admin.IsActive = false;
            _context.Admins.Update(admin);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRefreshTokenAsync(int adminId, string refreshToken, DateTime expiry)
        {
            var admin = await _context.Admins.FindAsync(adminId);
            if (admin == null) return;
            admin.RefreshToken = refreshToken;
            admin.RefreshTokenExpiryTime = expiry;
            await _context.SaveChangesAsync();
        }

        public async Task<Admin?> GetByRefreshTokenAsync(string refreshToken)
        {
            return await _context.Admins.FirstOrDefaultAsync(x => x.RefreshToken == refreshToken);
        }
    }
}