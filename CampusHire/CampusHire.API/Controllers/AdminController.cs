using System.Security.Claims;
using CampusHire.API.DTOs.Admin;
using CampusHire.API.DTOs.Auth;
using CampusHire.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CampusHire.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _service;

        public AdminController(IAdminService service)
        {
            _service = service;
        }


        [HttpPost("register")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Register(RegisterAdminDto dto)
        {
            return Ok(await _service.RegisterAsync(dto));
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _service.GetByIdAsync(id));
        }


        [HttpGet("profile")]
        public async Task<IActionResult> Profile()
        {
            var id = int.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier)!
            );

            return Ok(await _service.GetProfileAsync(id));
        }


        [HttpPut("profile/{id}")]
        public async Task<IActionResult> UpdateProfile(
            int id,
            UpdateAdminProfileDto dto)
        {
            return Ok(await _service.UpdateProfileAsync(id, dto));
        }


        [HttpPut("role/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateRole(
            int id,
            string role)
        {
            return Ok(await _service.UpdateRoleAsync(id, role));
        }


        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePassword(
            ChangePasswordDto dto)
        {
            var id = int.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier)!
            );

            await _service.ChangePasswordAsync(id, dto);

            return Ok("Password Changed Successfully");
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);

            return Ok("Admin Deleted Successfully");
        }


        [HttpPut("{id}/activate")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Activate(int id)
        {
            await _service.ActivateAsync(id);

            return Ok("Admin Activated Successfully");
        }


        [HttpPut("{id}/deactivate")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Deactivate(int id)
        {
            await _service.DeactivateAsync(id);

            return Ok("Admin Deactivated Successfully");
        }


        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(
            RefreshTokenDto dto)
        {
            return Ok(await _service.RefreshTokenAsync(dto));
        }


        [AllowAnonymous]
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(
            ForgotPasswordDto dto)
        {
            var token = await _service.ForgotPasswordAsync(dto);

            return Ok(new
            {
                Message = "Password reset token generated successfully.",
                ResetToken = token
            });
        }


        [AllowAnonymous]
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(
            ResetPasswordDto dto)
        {
            await _service.ResetPasswordAsync(dto);

            return Ok("Password reset successfully.");
        }
    }
}