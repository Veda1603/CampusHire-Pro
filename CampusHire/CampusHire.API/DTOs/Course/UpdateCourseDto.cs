namespace CampusHire.API.DTOs.Course
{
    public class UpdateCourseDto
    {
        public string CourseName { get; set; } = string.Empty;
        public int DepartmentId { get; set; }
        public int Duration { get; set; }
    }
}