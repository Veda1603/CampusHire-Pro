using CampusHire.API.Data;
using CampusHire.API.Services.Interfaces;
using ClosedXML.Excel;
using CsvHelper;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using System.Globalization;

namespace CampusHire.API.Services.Implementations
{
    public class ExportService : IExportService
    {
        private readonly CampusHireDbContext _context;
        public ExportService(CampusHireDbContext context)
        {
            _context = context;
        }
        // ===================== DEPARTMENT EXPORTS =====================
        public byte[] ExportDepartmentsToCsv()
        {
            using var memory = new MemoryStream();
            using var writer = new StreamWriter(memory);
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.WriteRecords(_context.Departments.ToList());
            writer.Flush();
            return memory.ToArray();
        }
        public byte[] ExportDepartmentsToExcel()
        {
            using var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Departments");
            ws.Cell(1, 1).Value = "Id";
            ws.Cell(1, 2).Value = "Department Name";
            ws.Cell(1, 3).Value = "Department Code";
            ws.Cell(1, 4).Value = "Status";
            var departments = _context.Departments.ToList();
            int row = 2;
            foreach (var d in departments)
            {
                ws.Cell(row, 1).Value = d.DepartmentId;
                ws.Cell(row, 2).Value = d.DepartmentName;
                ws.Cell(row, 3).Value = d.DepartmentCode;
                ws.Cell(row, 4).Value = d.IsActive ? "Active" : "Inactive";
                row++;
            }
            ws.Columns().AdjustToContents();
            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return stream.ToArray();
        }
        public byte[] ExportDepartmentsToPdf()
        {
            QuestPDF.Settings.License = LicenseType.Community;
            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);
                    page.Header().Text("Departments Report").FontSize(20).Bold();
                    page.Content().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                        });
                        table.Header(header =>
                        {
                            header.Cell().Text("Department").Bold();
                            header.Cell().Text("Code").Bold();
                            header.Cell().Text("Status").Bold();
                        });
                        foreach (var d in _context.Departments.ToList())
                        {
                            table.Cell().Text(d.DepartmentName);
                            table.Cell().Text(d.DepartmentCode);
                            table.Cell().Text(d.IsActive ? "Active" : "Inactive");
                        }
                    });
                    page.Footer().AlignCenter().Text("CampusHire Admin System");
                });
            }).GeneratePdf();
        }
        // ===================== COURSE EXPORTS =====================
        public byte[] ExportCoursesToCsv()
        {
            using var memory = new MemoryStream();
            using var writer = new StreamWriter(memory);
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.WriteRecords(_context.Courses.ToList());
            writer.Flush();
            return memory.ToArray();
        }
        public byte[] ExportCoursesToExcel()
        {
            using var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Courses");
            ws.Cell(1, 1).Value = "CourseId";
            ws.Cell(1, 2).Value = "CourseName";
            ws.Cell(1, 3).Value = "Duration";
            ws.Cell(1, 4).Value = "DepartmentId";
            var courses = _context.Courses.ToList();
            int row = 2;
            foreach (var c in courses)
            {
                ws.Cell(row, 1).Value = c.CourseId;
                ws.Cell(row, 2).Value = c.CourseName;
                ws.Cell(row, 3).Value = c.Duration;
                ws.Cell(row, 4).Value = c.DepartmentId;
                row++;
            }
            ws.Columns().AdjustToContents();
            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return stream.ToArray();
        }
        public byte[] ExportCoursesToPdf()
        {
            QuestPDF.Settings.License = LicenseType.Community;
            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);
                    page.Header().Text("Courses Report").FontSize(20).Bold();
                    page.Content().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                        });
                        table.Header(header =>
                        {
                            header.Cell().Text("Course").Bold();
                            header.Cell().Text("Duration").Bold();
                            header.Cell().Text("Department").Bold();
                        });
                        foreach (var c in _context.Courses.ToList())
                        {
                            table.Cell().Text(c.CourseName);
                            table.Cell().Text(c.Duration.ToString());
                            table.Cell().Text(c.Department != null ? c.Department.DepartmentName : c.DepartmentId.ToString());
                        }
                    });
                    page.Footer()
                        .AlignCenter()
                        .Text("CampusHire Admin System");
                });
            }).GeneratePdf();
        }
        // ======== PART 2 STARTS FROM HERE ========
        public byte[] ExportPlacementDrivesToCsv()
        {
            using var memory = new MemoryStream();
            using var writer = new StreamWriter(memory);
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.WriteRecords(_context.PlacementDrives.ToList());
            writer.Flush();
            return memory.ToArray();
        }
        public byte[] ExportPlacementDrivesToExcel()
        {
            using var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Placement Drives");
            ws.Cell(1, 1).Value = "DriveId";
            ws.Cell(1, 2).Value = "CompanyName";
            ws.Cell(1, 3).Value = "Location";
            ws.Cell(1, 4).Value = "Description";
            ws.Cell(1, 5).Value = "DriveDate";
            var drives = _context.PlacementDrives.ToList();
            int row = 2;
            foreach (var d in drives)
            {
                ws.Cell(row, 1).Value = d.DriveId;
                ws.Cell(row, 2).Value = d.CompanyName;
                ws.Cell(row, 3).Value = d.Location;
                ws.Cell(row, 4).Value = d.Description;
                ws.Cell(row, 5).Value = d.DriveDate;
                row++;
            }
            ws.Columns().AdjustToContents();
            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return stream.ToArray();
        }
        public byte[] ExportPlacementDrivesToPdf()
        {
            QuestPDF.Settings.License = LicenseType.Community;
            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);
                    page.Header().Text("Placement Drives Report").FontSize(20).Bold();
                    page.Content().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                        });
                        table.Header(header =>
                        {
                            header.Cell().Text("Company").Bold();
                            header.Cell().Text("Location").Bold();
                            header.Cell().Text("Description").Bold();
                            header.Cell().Text("Date").Bold();
                        });
                        foreach (var d in _context.PlacementDrives.ToList())
                        {
                            table.Cell().Text(d.CompanyName);
                            table.Cell().Text(d.Location);
                            table.Cell().Text(d.Description);
                            table.Cell().Text(d.DriveDate.ToShortDateString());
                        }
                    });
                    page.Footer().AlignCenter().Text("CampusHire Admin System");
                });
            }).GeneratePdf();
        }
        public byte[] ExportStudentVerificationsToCsv()
        {
            using var memory = new MemoryStream();
            using var writer = new StreamWriter(memory);
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.WriteRecords(_context.StudentVerifications.ToList());
            writer.Flush();
            return memory.ToArray();
        }
        public byte[] ExportStudentVerificationsToExcel()
        {
            using var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Student Verifications");
            ws.Cell(1, 1).Value = "VerificationId";
            ws.Cell(1, 2).Value = "StudentId";
            ws.Cell(1, 3).Value = "Status";
            ws.Cell(1, 4).Value = "VerifiedDate";
            var verifications = _context.StudentVerifications.ToList();
            int row = 2;
            foreach (var v in verifications)
            {
                ws.Cell(row, 1).Value = v.VerificationId;
                ws.Cell(row, 2).Value = v.StudentId;
                ws.Cell(row, 3).Value = v.Status;
                ws.Cell(row, 4).Value = v.VerifiedOn;
                row++;
            }
            ws.Columns().AdjustToContents();
            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return stream.ToArray();
        }
        public byte[] ExportStudentVerificationsToPdf()
        {
            QuestPDF.Settings.License = LicenseType.Community;
            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);
                    page.Header().Text("Student Verification Report").FontSize(20).Bold();
                    page.Content().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                        });
                        table.Header(header =>
                        {
                            header.Cell().Text("Student Id").Bold();
                            header.Cell().Text("Status").Bold();
                            header.Cell().Text("Verified On").Bold();
                        });
                        foreach (var v in _context.StudentVerifications.ToList())
                        {
                            table.Cell().Text(v.StudentId.ToString());
                            table.Cell().Text(v.Status);
                            table.Cell().Text(v.VerifiedOn.ToShortDateString());
                        }
                    });
                    page.Footer().AlignCenter().Text("CampusHire Admin System");
                });
            }).GeneratePdf();
        }
    }
}