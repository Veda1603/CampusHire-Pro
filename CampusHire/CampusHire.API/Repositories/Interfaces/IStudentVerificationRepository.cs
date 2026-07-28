using CampusHire.API.Entities;

namespace CampusHire.API.Repositories.Interfaces
{
    public interface IStudentVerificationRepository
    {
        Task<List<StudentVerification>> GetAllAsync();
        Task<StudentVerification?> GetByIdAsync(int id);
        Task AddAsync(StudentVerification verification);
        Task UpdateAsync(StudentVerification verification);
        Task DeleteAsync(StudentVerification verification);
        Task SaveChangesAsync();
    }
}