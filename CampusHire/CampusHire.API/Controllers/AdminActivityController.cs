using CampusHire.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CampusHire.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AdminActivityController : ControllerBase
    {
        private readonly IAdminActivityService _service;
        public AdminActivityController(
            IAdminActivityService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{adminId}")]
        public async Task<IActionResult> GetByAdminId(int adminId)
        {
            return Ok(
                await _service.GetByAdminIdAsync(adminId)
            );
        }

        [HttpPost]
        public async Task<IActionResult> Add(
            int adminId,
            string action,
            string description)
        {
            await _service.AddAsync(
                adminId,
                action,
                description
            );
            return Ok("Activity Added");
        }
    }
}