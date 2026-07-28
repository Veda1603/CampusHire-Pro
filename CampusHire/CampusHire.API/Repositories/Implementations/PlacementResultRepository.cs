using CampusHire.API.Data;
using CampusHire.API.Entities;
using CampusHire.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace CampusHire.API.Repositories.Implementations
{
    public class PlacementResultRepository : IPlacementResultRepository
    {
        private readonly CampusHireDbContext _context;
        public PlacementResultRepository(CampusHireDbContext context)
        {
            _context = context;
        }
        public async Task<List<PlacementResult>> GetAllAsync()
        {
            return await _context.PlacementResults.ToListAsync();
        }
        public async Task<PlacementResult?> GetByIdAsync(int id)
        {
            return await _context.PlacementResults.FindAsync(id);
        }
        public async Task AddAsync(PlacementResult result)
        {
            await _context.PlacementResults.AddAsync(result);
        }
        public Task UpdateAsync(PlacementResult result)
        {
            _context.PlacementResults.Update(result);
            return Task.CompletedTask;
        }
        public Task DeleteAsync(PlacementResult result)
        {
            _context.PlacementResults.Remove(result);
            return Task.CompletedTask;
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}