using CampusHire.API.DTOs.Sms;

namespace CampusHire.API.Services.Interfaces
{
    public interface ISmsService
    {
        Task<string> SendSmsAsync(SmsRequestDto dto);
    }
}