using System.ComponentModel.DataAnnotations;

namespace CampusHire.API.DTOs.Sms
{
    public class SmsRequestDto
    {
        [Required]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        public string Message { get; set; } = string.Empty;
    }
}