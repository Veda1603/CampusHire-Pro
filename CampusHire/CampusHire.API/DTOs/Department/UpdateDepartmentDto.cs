namespace CampusHire.API.DTOs.Department
{
    public class UpdateDepartmentDto
    {
        public string DepartmentName { get; set; } = string.Empty;
        public string DepartmentCode { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}