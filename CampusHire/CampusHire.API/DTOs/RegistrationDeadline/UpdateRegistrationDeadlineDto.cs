namespace CampusHire.API.DTOs.RegistrationDeadline
{
    public class UpdateRegistrationDeadlineDto
    {
        public string DriveName { get; set; } = string.Empty;

        public DateTime RegistrationStartDate { get; set; }

        public DateTime RegistrationEndDate { get; set; }

        public string Status { get; set; } = "Active";
    }
}