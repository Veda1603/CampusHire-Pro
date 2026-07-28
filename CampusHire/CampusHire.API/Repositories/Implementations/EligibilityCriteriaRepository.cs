using CampusHire.API.Data;
using CampusHire.API.Entities;
using CampusHire.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CampusHire.API.Repositories.Implementations
{
    public class EligibilityCriteriaRepository : IEligibilityCriteriaRepository
    {
        private readonly CampusHireDbContext _context;
        public EligibilityCriteriaRepository(CampusHireDbContext context)
        {
            _context = context;
        }
        public async Task<List<EligibilityCriteria>> GetAllAsync()
        {
            return await _context.EligibilityCriterias.ToListAsync();
        }
        public async Task<EligibilityCriteria?> GetByIdAsync(int id)
        {
            return await _context.EligibilityCriterias.FindAsync(id);
        }
        public async Task AddAsync(EligibilityCriteria criteria)
        {
            await _context.EligibilityCriterias.AddAsync(criteria);
        }
        public Task UpdateAsync(EligibilityCriteria criteria)
        {
            _context.EligibilityCriterias.Update(criteria);
            return Task.CompletedTask;
        }
        public Task DeleteAsync(EligibilityCriteria criteria)
        {
            _context.EligibilityCriterias.Remove(criteria);
            return Task.CompletedTask;
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}