using CampusHire.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CampusHire.API.Controllers
{
    [ApiController]
    [Route("api/email")]
    public class EmailVerificationController : ControllerBase
    {
        private readonly CampusHireDbContext _context;

        public EmailVerificationController(CampusHireDbContext context)
        {
            _context = context;
        }

        [HttpGet("verify")]
        public async Task<IActionResult> Verify(string token)
        {
            var admin = await _context.Admins
                .FirstOrDefaultAsync(x =>
                    x.EmailVerificationToken == token);

            if (admin == null)
                return BadRequest("Invalid token");

            if (admin.EmailVerificationExpiry < DateTime.UtcNow)
                return BadRequest("Token expired");

            admin.IsEmailVerified = true;
            admin.EmailVerificationToken = null;
            admin.EmailVerificationExpiry = null;

            await _context.SaveChangesAsync();

            return Ok("Email verified successfully");
        }
    }
}