using System.ComponentModel.DataAnnotations;

namespace CampusHire.API.DTOs.Admin
{
    public class RegisterAdminDto
    {
        [Required]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        public string Role { get; set; } = "TPO";
    }
}