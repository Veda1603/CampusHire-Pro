using System.ComponentModel.DataAnnotations;

namespace CampusHire.API.DTOs.Auth
{
    public class SendOtpDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}