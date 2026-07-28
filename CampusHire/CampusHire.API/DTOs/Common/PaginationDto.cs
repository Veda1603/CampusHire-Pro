namespace CampusHire.API.DTOs.Common
{
    public class PaginationDto
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? Search { get; set; }
        public string? SortBy { get; set; }
        public bool Desc { get; set; } = false;

        // Department
        public bool? IsActive { get; set; }

        // Course
        public int? DepartmentId { get; set; }
    }
}