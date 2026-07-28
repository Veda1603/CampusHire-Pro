using System.ComponentModel.DataAnnotations;

namespace CampusHire.API.Entities
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }

        [Required]
        [StringLength(100)]
        public string DepartmentName { get; set; } = string.Empty;

        [StringLength(20)]
        public string DepartmentCode { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;
    }
}