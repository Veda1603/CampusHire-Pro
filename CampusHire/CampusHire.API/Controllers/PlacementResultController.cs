using CampusHire.API.DTOs.PlacementResult;
using CampusHire.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace CampusHire.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlacementResultController : ControllerBase
    {
        private readonly IPlacementResultService _service;
        public PlacementResultController(IPlacementResultService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreatePlacementResultDto dto)
        {
            return Ok(await _service.CreateAsync(dto));
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdatePlacementResultDto dto)
        {
            return Ok(await _service.UpdateAsync(id, dto));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _service.DeleteAsync(id));
        }
    }
}