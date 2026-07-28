using System.ComponentModel.DataAnnotations;

namespace CampusHire.API.DTOs.Course
{
    public class CreateCourseDto
    {
        [Required]
        public string CourseName { get; set; } = string.Empty;

        [Required]
        public int DepartmentId { get; set; }

        [Range(1, 10)]
        public int Duration { get; set; }
    }
}