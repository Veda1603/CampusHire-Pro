using CampusHire.API.Entities;

namespace CampusHire.API.Repositories.Interfaces
{
    public interface IRegistrationDeadlineRepository
    {
        Task<List<RegistrationDeadline>> GetAllAsync();
        Task<RegistrationDeadline?> GetByIdAsync(int id);
        Task AddAsync(RegistrationDeadline deadline);
        Task UpdateAsync(RegistrationDeadline deadline);
        Task DeleteAsync(RegistrationDeadline deadline);
        Task SaveChangesAsync();
    }
}