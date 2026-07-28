using CampusHire.API.Authentication;
using CampusHire.API.Data;
using CampusHire.API.DTOs.Auth;
using CampusHire.API.DTOs.Admin;
using CampusHire.API.Helpers;
using CampusHire.API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CampusHire.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly CampusHireDbContext _context;
        private readonly JwtService _jwtService;
        private readonly IPasswordResetService _passwordResetService;

        public AuthController(CampusHireDbContext context, JwtService jwtService, IPasswordResetService passwordResetService)
        {
            _context = context;
            _jwtService = jwtService;
            _passwordResetService = passwordResetService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto request)
        {
            var admin = await _context.Admins.FirstOrDefaultAsync(a => a.Email == request.Email);

            if (admin == null)
                return Unauthorized("Invalid email or password");

            if (!admin.IsActive)
                return Unauthorized("Account is deactivated");

            if (admin.LockoutEnd != null && admin.LockoutEnd > DateTime.UtcNow)
                return Unauthorized("Account is locked due to multiple failed login attempts. Please try again after 5 minutes.");

            if (!PasswordHelper.VerifyPassword(request.Password, admin.PasswordHash))
            {
                admin.FailedLoginAttempts++;

                if (admin.FailedLoginAttempts >= 5)
                {
                    admin.LockoutEnd = DateTime.UtcNow.AddMinutes(5);
                    admin.FailedLoginAttempts = 0;
                }

                await _context.SaveChangesAsync();
                return Unauthorized("Invalid email or password");
            }

            admin.FailedLoginAttempts = 0;
            admin.LockoutEnd = null;

            var accessToken = _jwtService.GenerateToken(admin);
            var refreshToken = _jwtService.GenerateRefreshToken();

            admin.RefreshToken = refreshToken;
            admin.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            await _context.SaveChangesAsync();

            return Ok(new LoginResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                FullName = admin.FullName,
                Email = admin.Email,
                Role = admin.Role
            });
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto dto)
        {
            var token = await _passwordResetService.GenerateResetToken(dto);
            return Ok(new
            {
                message = "Password reset token generated successfully",
                token = token
            });
        }
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto dto)
        {
            var result = await _passwordResetService.ResetPassword(dto);
            if (!result)
                return BadRequest(new
                {
                    message = "Invalid or expired token"
                });
            return Ok(new
            {
                message = "Password reset successfully"
            });
        }
    }
}