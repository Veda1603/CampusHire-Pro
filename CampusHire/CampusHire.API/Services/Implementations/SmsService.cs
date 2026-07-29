using CampusHire.API.DTOs.Sms;
using CampusHire.API.Services.Interfaces;

namespace CampusHire.API.Services.Implementations
{
    public class SmsService : ISmsService
    {
        private readonly ILogger<SmsService> _logger;

        public SmsService(ILogger<SmsService> logger)
        {
            _logger = logger;
        }

        public async Task<string> SendSmsAsync(SmsRequestDto dto)
        {
            _logger.LogInformation(
                "SMS To: {Phone} | Message: {Message}",
                dto.PhoneNumber,
                dto.Message);

            await Task.CompletedTask;

            return "SMS request logged successfully (provider integration pending).";
        }
    }
}