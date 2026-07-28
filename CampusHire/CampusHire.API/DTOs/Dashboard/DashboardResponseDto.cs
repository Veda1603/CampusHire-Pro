namespace CampusHire.API.DTOs.Dashboard
{
    public class DashboardResponseDto
    {
        public int TotalDepartments { get; set; }
        public int ActiveDepartments { get; set; }
        public int TotalCourses { get; set; }
        public int TotalPlacementDrives { get; set; }
        public int ActivePlacementDrives { get; set; }
        public int TotalStudentVerifications { get; set; }
        public int PendingVerifications { get; set; }
    }
}