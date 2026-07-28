using CampusHire.API.Entities;
using CampusHire.API.DTOs.Common;
namespace CampusHire.API.Repositories.Interfaces
{
    public interface ICourseRepository
    {
        Task<List<Course>> GetAllAsync();
        Task<Course?> GetByIdAsync(int id);
        Task AddAsync(Course course);
        Task UpdateAsync(Course course);
        Task DeleteAsync(Course course);
        Task SaveChangesAsync();
        Task<IEnumerable<Course>> GetPagedAsync(PaginationDto dto);
    }
}