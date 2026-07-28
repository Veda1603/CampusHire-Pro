using CampusHire.API.Data;
using CampusHire.API.Entities;
using CampusHire.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using CampusHire.API.DTOs.Common;

namespace CampusHire.API.Repositories.Implementations
{
    public class CourseRepository : ICourseRepository
    {
        private readonly CampusHireDbContext _context;

        public CourseRepository(CampusHireDbContext context)
        {
            _context = context;
        }

        public async Task<List<Course>> GetAllAsync()
        {
            return await _context.Courses.Include(c => c.Department).ToListAsync();
        }

        public async Task<Course?> GetByIdAsync(int id)
        {
            return await _context.Courses.Include(c => c.Department).FirstOrDefaultAsync(c => c.CourseId == id);
        }

        public async Task AddAsync(Course course)
        {
            await _context.Courses.AddAsync(course);
        }

        public Task UpdateAsync(Course course)
        {
            _context.Courses.Update(course);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Course course)
        {
            _context.Courses.Remove(course);
            return Task.CompletedTask;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Course>> GetPagedAsync(PaginationDto dto)
        {
            var query = _context.Courses.AsQueryable();
            if (!string.IsNullOrWhiteSpace(dto.Search))
                query = query.Where(x => x.CourseName.Contains(dto.Search));
            if (dto.DepartmentId.HasValue)
                query = query.Where(x => x.DepartmentId == dto.DepartmentId);
            query = dto.SortBy?.ToLower() switch
            {
                "duration" => dto.Desc ? query.OrderByDescending(x => x.Duration) : query.OrderBy(x => x.Duration),
                _ => dto.Desc ? query.OrderByDescending(x => x.CourseName) : query.OrderBy(x => x.CourseName)
            };
            return await query.Skip((dto.PageNumber - 1) * dto.PageSize).Take(dto.PageSize).ToListAsync();
        }
    }
}