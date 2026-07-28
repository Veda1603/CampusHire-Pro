using CampusHire.API.DTOs.Common;
using CampusHire.API.DTOs.Department;
namespace CampusHire.API.Services.Interfaces
{
    public interface IDepartmentService
    {
        Task<List<DepartmentDto>> GetAllAsync();
        Task<DepartmentDto?> GetByIdAsync(int id);
        Task<string> CreateAsync(CreateDepartmentDto dto);
        Task<string> UpdateAsync(int id, UpdateDepartmentDto dto);
        Task<string> DeleteAsync(int id);
        Task<IEnumerable<DepartmentDto>> GetPagedAsync(PaginationDto dto);
    }
}