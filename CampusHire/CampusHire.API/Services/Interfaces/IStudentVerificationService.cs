using CampusHire.API.DTOs.Student;

namespace CampusHire.API.Services.Interfaces
{
    public interface IStudentVerificationService
    {
        Task<List<StudentVerificationDto>> GetAllAsync();
        Task<StudentVerificationDto?> GetByIdAsync(int id);
        Task<string> CreateAsync(CreateStudentVerificationDto dto);
        Task<string> UpdateAsync(int id, UpdateStudentVerificationDto dto);
        Task<string> DeleteAsync(int id);
    }
}