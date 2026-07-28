using CampusHire.API.Entities;
using CampusHire.API.DTOs.Common;

namespace CampusHire.API.Repositories.Interfaces
{
    public interface IDepartmentRepository
    {
        Task<List<Department>> GetAllAsync();
        Task<Department?> GetByIdAsync(int id);
        Task AddAsync(Department department);
        Task UpdateAsync(Department department);
        Task DeleteAsync(Department department);
        Task<bool> ExistsByCodeAsync(string code);
        Task<IEnumerable<Department>> GetPagedAsync(PaginationDto dto);
        Task SaveChangesAsync();
    }
}