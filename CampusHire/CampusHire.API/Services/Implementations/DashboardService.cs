using CampusHire.API.Data;
using CampusHire.API.DTOs.Dashboard;
using CampusHire.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CampusHire.API.Services.Implementations
{
    public class DashboardService : IDashboardService
    {
        private readonly CampusHireDbContext _context;
        public DashboardService(CampusHireDbContext context)
        {
            _context = context;
        }
        public async Task<DashboardResponseDto> GetDashboardAsync()
        {
            return new DashboardResponseDto
            {
                TotalDepartments = await _context.Departments.CountAsync(),
                ActiveDepartments = await _context.Departments.CountAsync(x => x.IsActive),
                TotalCourses = await _context.Courses.CountAsync(),
                TotalPlacementDrives = await _context.PlacementDrives.CountAsync(),
                ActivePlacementDrives = await _context.PlacementDrives.CountAsync(x => x.IsActive),
                TotalStudentVerifications = await _context.StudentVerifications.CountAsync(),
                PendingVerifications = await _context.StudentVerifications.CountAsync(x => x.Status == "Pending")
            };
        }
    }
}