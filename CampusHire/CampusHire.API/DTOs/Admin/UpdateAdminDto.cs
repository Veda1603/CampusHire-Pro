using System.ComponentModel.DataAnnotations;

namespace CampusHire.API.DTOs.Admin
{
    public class UpdateAdminDto
    {
        [Required]
        [MaxLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}