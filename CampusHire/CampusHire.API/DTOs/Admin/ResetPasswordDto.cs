using System.ComponentModel.DataAnnotations;

namespace CampusHire.API.DTOs.Admin
{
    public class ResetPasswordDto
    {
        [Required]
        public string Token { get; set; } = string.Empty;
        [Required]
        public string NewPassword { get; set; } = string.Empty;
    }
}