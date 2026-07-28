using CampusHire.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CampusHire.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ExportController : ControllerBase
    {
        private readonly IExportService _exportService;

        public ExportController(IExportService exportService)
        {
            _exportService = exportService;
        }

        // ===================== DEPARTMENT EXPORT =====================

        [HttpGet("departments/csv")]
        public IActionResult ExportDepartmentsToCsv()
        {
            var file = _exportService.ExportDepartmentsToCsv();
            return File(file, "text/csv", "Departments.csv");
        }

        [HttpGet("departments/excel")]
        public IActionResult ExportDepartmentsToExcel()
        {
            var file = _exportService.ExportDepartmentsToExcel();

            return File(
                file,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "Departments.xlsx");
        }

        [HttpGet("departments/pdf")]
        public IActionResult ExportDepartmentsToPdf()
        {
            var file = _exportService.ExportDepartmentsToPdf();
            return File(file, "application/pdf", "Departments.pdf");
        }


        // ===================== COURSE EXPORT =====================

        [HttpGet("courses/csv")]
        public IActionResult ExportCoursesToCsv()
        {
            var file = _exportService.ExportCoursesToCsv();
            return File(file, "text/csv", "Courses.csv");
        }

        [HttpGet("courses/excel")]
        public IActionResult ExportCoursesToExcel()
        {
            var file = _exportService.ExportCoursesToExcel();

            return File(
                file,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "Courses.xlsx");
        }

        [HttpGet("courses/pdf")]
        public IActionResult ExportCoursesToPdf()
        {
            var file = _exportService.ExportCoursesToPdf();
            return File(file, "application/pdf", "Courses.pdf");
        }


        // ===================== PLACEMENT DRIVE EXPORT =====================

        [HttpGet("placement-drives/csv")]
        public IActionResult ExportPlacementDrivesToCsv()
        {
            var file = _exportService.ExportPlacementDrivesToCsv();
            return File(file, "text/csv", "PlacementDrives.csv");
        }

        [HttpGet("placement-drives/excel")]
        public IActionResult ExportPlacementDrivesToExcel()
        {
            var file = _exportService.ExportPlacementDrivesToExcel();

            return File(
                file,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "PlacementDrives.xlsx");
        }

        [HttpGet("placement-drives/pdf")]
        public IActionResult ExportPlacementDrivesToPdf()
        {
            var file = _exportService.ExportPlacementDrivesToPdf();
            return File(file, "application/pdf", "PlacementDrives.pdf");
        }


        // ===================== STUDENT VERIFICATION EXPORT =====================

        [HttpGet("student-verifications/csv")]
        public IActionResult ExportStudentVerificationsToCsv()
        {
            var file = _exportService.ExportStudentVerificationsToCsv();
            return File(file, "text/csv", "StudentVerifications.csv");
        }

        [HttpGet("student-verifications/excel")]
        public IActionResult ExportStudentVerificationsToExcel()
        {
            var file = _exportService.ExportStudentVerificationsToExcel();

            return File(
                file,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "StudentVerifications.xlsx");
        }

        [HttpGet("student-verifications/pdf")]
        public IActionResult ExportStudentVerificationsToPdf()
        {
            var file = _exportService.ExportStudentVerificationsToPdf();
            return File(file, "application/pdf", "StudentVerifications.pdf");
        }
    }
}