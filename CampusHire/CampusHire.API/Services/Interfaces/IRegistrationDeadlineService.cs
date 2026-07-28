using CampusHire.API.DTOs.RegistrationDeadline;

namespace CampusHire.API.Services.Interfaces
{
    public interface IRegistrationDeadlineService
    {
        Task<List<RegistrationDeadlineDto>> GetAllAsync();
        Task<RegistrationDeadlineDto?> GetByIdAsync(int id);
        Task<string> CreateAsync(CreateRegistrationDeadlineDto dto);
        Task<string> UpdateAsync(int id, UpdateRegistrationDeadlineDto dto);
        Task<string> DeleteAsync(int id);
    }
}