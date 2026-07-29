using CampusHire.API.DTOs.Auth;

namespace CampusHire.API.Services.Interfaces
{
    public interface IOtpService
    {
        Task<string> SendOtpAsync(SendOtpDto dto);
        Task<string> VerifyOtpAsync(VerifyOtpDto dto);
    }
}