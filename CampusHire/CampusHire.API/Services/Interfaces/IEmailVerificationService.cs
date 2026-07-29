namespace CampusHire.API.Services.Interfaces
{
    public interface IEmailVerificationService
    {
        Task<string> VerifyEmailAsync(string token);
    }
}