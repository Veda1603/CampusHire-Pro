using System.ComponentModel.DataAnnotations;

namespace CampusHire.API.DTOs.RegistrationDeadline
{
    public class CreateRegistrationDeadlineDto
    {
        [Required]
        public string DriveName { get; set; } = string.Empty;

        public DateTime RegistrationStartDate { get; set; }

        public DateTime RegistrationEndDate { get; set; }

        public string Status { get; set; } = "Active";
    }
}