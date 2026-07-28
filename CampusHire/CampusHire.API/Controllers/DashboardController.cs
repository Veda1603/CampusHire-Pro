using CampusHire.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CampusHire.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController:ControllerBase
    {
        private readonly IDashboardService _service;
        public DashboardController(IDashboardService service)
        {
            _service=service;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.GetDashboardAsync());
        }
    }
}