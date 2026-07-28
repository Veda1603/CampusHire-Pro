using CampusHire.API.Data;
using CampusHire.API.DTOs.Common;
using CampusHire.API.Entities;
using CampusHire.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CampusHire.API.Repositories.Implementations
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly CampusHireDbContext _context;

        public DepartmentRepository(CampusHireDbContext context)
        {
            _context = context;
        }

        public async Task<List<Department>> GetAllAsync()
        {
            return await _context.Departments.ToListAsync();
        }

        public async Task<Department?> GetByIdAsync(int id)
        {
            return await _context.Departments.FindAsync(id);
        }

        public async Task AddAsync(Department department)
        {
            await _context.Departments.AddAsync(department);
        }

        public Task UpdateAsync(Department department)
        {
            _context.Departments.Update(department);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Department department)
        {
            _context.Departments.Remove(department);
            return Task.CompletedTask;
        }

        public async Task<bool> ExistsByCodeAsync(string code)
        {
            return await _context.Departments.AnyAsync(d => d.DepartmentCode == code);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Department>> GetPagedAsync(PaginationDto dto)
        {
            var query = _context.Departments.AsQueryable();

            if (!string.IsNullOrWhiteSpace(dto.Search))
                query = query.Where(x => x.DepartmentName.Contains(dto.Search) || x.DepartmentCode.Contains(dto.Search));

            if (dto.IsActive.HasValue)
                query = query.Where(x => x.IsActive == dto.IsActive.Value);

            query = dto.SortBy?.ToLower() switch
            {
                "departmentcode" => dto.Desc ? query.OrderByDescending(x => x.DepartmentCode) : query.OrderBy(x => x.DepartmentCode),
                _ => dto.Desc ? query.OrderByDescending(x => x.DepartmentName) : query.OrderBy(x => x.DepartmentName)
            };

            return await query.Skip((dto.PageNumber - 1) * dto.PageSize).Take(dto.PageSize).ToListAsync();
        }
    }
}