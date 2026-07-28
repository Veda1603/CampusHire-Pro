using CampusHire.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CampusHire.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _service;
        private readonly IExportService _exportService;

        public ReportController(IReportService service, IExportService exportService)
        {
            _service = service;
            _exportService = exportService;
        }

        [HttpGet]
        public async Task<IActionResult> GetReport()
        {
            return Ok(await _service.GetReportAsync());
        }

        [HttpGet("departments/csv")]
        public IActionResult ExportDepartmentsCsv()
        {
            return File(
                _exportService.ExportDepartmentsToCsv(),
                "text/csv",
                "Departments.csv");
        }

        [HttpGet("departments/excel")]
        public IActionResult ExportDepartmentsExcel()
        {
            return File(
                _exportService.ExportDepartmentsToExcel(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "Departments.xlsx");
        }

        [HttpGet("departments/pdf")]
        public IActionResult ExportDepartmentsPdf()
        {
            return File(
                _exportService.ExportDepartmentsToPdf(),
                "application/pdf",
                "Departments.pdf");
        }
    }
}