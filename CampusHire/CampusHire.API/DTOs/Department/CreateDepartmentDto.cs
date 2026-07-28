using System.ComponentModel.DataAnnotations;

namespace CampusHire.API.DTOs.Department
{
    public class CreateDepartmentDto
    {
        [Required]
        [MaxLength(100)]
        public string DepartmentName { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string DepartmentCode { get; set; } = string.Empty;
    }
}