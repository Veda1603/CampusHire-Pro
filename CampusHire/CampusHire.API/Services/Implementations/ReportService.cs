using CampusHire.API.Data;
using CampusHire.API.DTOs.Report;
using CampusHire.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CampusHire.API.Services.Implementations
{
    public class ReportService : IReportService
    {
        private readonly CampusHireDbContext _context;

        public ReportService(CampusHireDbContext context)
        {
            _context = context;
        }

        public async Task<ReportDto> GetReportAsync()
        {
            return new ReportDto
            {
                TotalDepartments = await _context.Departments.CountAsync(),
                TotalCourses = await _context.Courses.CountAsync(),
                TotalPlacementDrives = await _context.PlacementDrives.CountAsync(),
                TotalVerifiedStudents = await _context.StudentVerifications.CountAsync(s => s.Status == "Approved")
            };
        }
    }
}