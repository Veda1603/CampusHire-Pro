using CampusHire.API.Data;
using CampusHire.API.Entities;
using CampusHire.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CampusHire.API.Repositories.Implementations
{
    public class PlacementDriveRepository : IPlacementDriveRepository
    {
        private readonly CampusHireDbContext _context;

        public PlacementDriveRepository(CampusHireDbContext context)
        {
            _context = context;
        }

        public async Task<List<PlacementDrive>> GetAllAsync()
        {
            return await _context.PlacementDrives.ToListAsync();
        }

        public async Task<PlacementDrive?> GetByIdAsync(int id)
        {
            return await _context.PlacementDrives.FindAsync(id);
        }

        public async Task AddAsync(PlacementDrive drive)
        {
            await _context.PlacementDrives.AddAsync(drive);
        }

        public Task UpdateAsync(PlacementDrive drive)
        {
            _context.PlacementDrives.Update(drive);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(PlacementDrive drive)
        {
            _context.PlacementDrives.Remove(drive);
            return Task.CompletedTask;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}