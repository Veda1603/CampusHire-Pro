using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CampusHire.API.Entities
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }

        [Required]
        [StringLength(100)]
        public string CourseName { get; set; } = string.Empty;

        [Required]
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }

        public int Duration { get; set; }

        public Department Department { get; set; } = null!;
    }
}