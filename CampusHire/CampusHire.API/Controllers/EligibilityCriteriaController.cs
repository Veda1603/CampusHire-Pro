using CampusHire.API.DTOs.Eligibility;
using CampusHire.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CampusHire.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EligibilityCriteriaController : ControllerBase
    {
        private readonly IEligibilityCriteriaService _service;

        public EligibilityCriteriaController(IEligibilityCriteriaService service)
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
        public async Task<IActionResult> Create(CreateEligibilityCriteriaDto dto)
        {
            return Ok(await _service.CreateAsync(dto));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateEligibilityCriteriaDto dto)
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