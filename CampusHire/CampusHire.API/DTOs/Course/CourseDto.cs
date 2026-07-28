namespace CampusHire.API.DTOs.Course
{
    public class CourseDto
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; } = string.Empty;
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; } = string.Empty;
        public int Duration { get; set; }
    }
}