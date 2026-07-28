using CampusHire.API.Data;
using CampusHire.API.Entities;
using CampusHire.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CampusHire.API.Repositories.Implementations
{
    public class StudentVerificationRepository : IStudentVerificationRepository
    {
        private readonly CampusHireDbContext _context;

        public StudentVerificationRepository(CampusHireDbContext context)
        {
            _context = context;
        }

        public async Task<List<StudentVerification>> GetAllAsync()
        {
            return await _context.StudentVerifications.ToListAsync();
        }

        public async Task<StudentVerification?> GetByIdAsync(int id)
        {
            return await _context.StudentVerifications.FindAsync(id);
        }

        public async Task AddAsync(StudentVerification verification)
        {
            await _context.StudentVerifications.AddAsync(verification);
        }

        public Task UpdateAsync(StudentVerification verification)
        {
            _context.StudentVerifications.Update(verification);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(StudentVerification verification)
        {
            _context.StudentVerifications.Remove(verification);
            return Task.CompletedTask;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}