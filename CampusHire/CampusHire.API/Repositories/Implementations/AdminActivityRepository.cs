using CampusHire.API.Data;
using CampusHire.API.Entities;
using CampusHire.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CampusHire.API.Repositories.Implementations
{
    public class AdminActivityRepository : IAdminActivityRepository
    {
        private readonly CampusHireDbContext _context;
        public AdminActivityRepository(CampusHireDbContext context)
        {
            _context = context;
        }
        public async Task<List<AdminActivityLog>> GetAllAsync()
        {
            return await _context.AdminActivityLogs
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }

        public async Task<List<AdminActivityLog>> GetByAdminIdAsync(int adminId)
        {
            return await _context.AdminActivityLogs
                .Where(x => x.AdminId == adminId)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }

        public async Task AddAsync(AdminActivityLog activity)
        {
            await _context.AdminActivityLogs.AddAsync(activity);
            await _context.SaveChangesAsync();
        }
    }
}