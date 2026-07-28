using CampusHire.API.DTOs.Common;
using CampusHire.API.DTOs.Course;

namespace CampusHire.API.Services.Interfaces
{
    public interface ICourseService
    {
        Task<List<CourseDto>> GetAllAsync();
        Task<CourseDto?> GetByIdAsync(int id);
        Task<string> CreateAsync(CreateCourseDto dto);
        Task<string> UpdateAsync(int id, UpdateCourseDto dto);
        Task<string> DeleteAsync(int id);
        Task<IEnumerable<CourseDto>> GetPagedAsync(PaginationDto dto);
    }
}