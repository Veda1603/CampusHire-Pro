using CampusHire.API.Data;
using CampusHire.API.Entities;
using CampusHire.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CampusHire.API.Repositories.Implementations
{
    public class RegistrationDeadlineRepository : IRegistrationDeadlineRepository
    {
        private readonly CampusHireDbContext _context;

        public RegistrationDeadlineRepository(CampusHireDbContext context)
        {
            _context = context;
        }

        public async Task<List<RegistrationDeadline>> GetAllAsync()
        {
            return await _context.RegistrationDeadlines.ToListAsync();
        }

        public async Task<RegistrationDeadline?> GetByIdAsync(int id)
        {
            return await _context.RegistrationDeadlines.FindAsync(id);
        }

        public async Task AddAsync(RegistrationDeadline deadline)
        {
            await _context.RegistrationDeadlines.AddAsync(deadline);
        }

        public Task UpdateAsync(RegistrationDeadline deadline)
        {
            _context.RegistrationDeadlines.Update(deadline);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(RegistrationDeadline deadline)
        {
            _context.RegistrationDeadlines.Remove(deadline);
            return Task.CompletedTask;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}