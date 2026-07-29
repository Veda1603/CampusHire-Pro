using CampusHire.API.DTOs.Sms;
using CampusHire.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CampusHire.API.Controllers
{
    [ApiController]
    [Route("api/sms")]
    [Authorize(Roles = "Admin")]
    public class SmsController : ControllerBase
    {
        private readonly ISmsService _service;

        public SmsController(ISmsService service)
        {
            _service = service;
        }

        [HttpPost("send")]
        public async Task<IActionResult> Send(SmsRequestDto dto)
        {
            var result = await _service.SendSmsAsync(dto);
            return Ok(result);
        }
    }
}