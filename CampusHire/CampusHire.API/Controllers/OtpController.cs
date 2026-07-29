using CampusHire.API.DTOs.Auth;
using CampusHire.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CampusHire.API.Controllers
{
    [ApiController]
    [Route("api/otp")]
    public class OtpController : ControllerBase
    {
        private readonly IOtpService _service;
        public OtpController(IOtpService service)
        {
            _service = service;
        }
        [HttpPost("send")]
        public async Task<IActionResult> SendOtp(SendOtpDto dto)
        {
            return Ok(await _service.SendOtpAsync(dto));
        }
        [HttpPost("verify")]
        public async Task<IActionResult> VerifyOtp(VerifyOtpDto dto)
        {
            return Ok(await _service.VerifyOtpAsync(dto));
        }
    }
}