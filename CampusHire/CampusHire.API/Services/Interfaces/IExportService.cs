namespace CampusHire.API.Services.Interfaces
{
    public interface IExportService
    {
        // Department
        byte[] ExportDepartmentsToCsv();
        byte[] ExportDepartmentsToExcel();
        byte[] ExportDepartmentsToPdf();

        // Course
        byte[] ExportCoursesToCsv();
        byte[] ExportCoursesToExcel();
        byte[] ExportCoursesToPdf();

        // Placement Drive
        byte[] ExportPlacementDrivesToCsv();
        byte[] ExportPlacementDrivesToExcel();
        byte[] ExportPlacementDrivesToPdf();

        // Student Verification
        byte[] ExportStudentVerificationsToCsv();
        byte[] ExportStudentVerificationsToExcel();
        byte[] ExportStudentVerificationsToPdf();
    }
}