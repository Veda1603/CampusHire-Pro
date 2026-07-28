using AutoMapper;
using BCrypt.Net;
using CampusHire.API.Authentication;
using CampusHire.API.DTOs.Admin;
using CampusHire.API.DTOs.Auth;
using CampusHire.API.Entities;
using CampusHire.API.Exceptions;
using CampusHire.API.Helpers;
using CampusHire.API.Repositories.Interfaces;
using CampusHire.API.Services.Interfaces;
using System.Security.Cryptography;

namespace CampusHire.API.Services.Implementations
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _repository;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;
        private readonly ActivityLogger _logger;
        private readonly JwtService _jwtService;

        public AdminService(
            IAdminRepository repository,
            IMapper mapper,
            IEmailService emailService)
        {
            _repository = repository;
            _mapper = mapper;
            _emailService = emailService;
        }

        public async Task<List<AdminDto>> GetAllAsync()
        {
            var admins = await _repository.GetAllAsync();
            return _mapper.Map<List<AdminDto>>(admins);
        }

        public async Task<AdminProfileDto?> GetByIdAsync(int id)
        {
            var admin = await _repository.GetByIdAsync(id);

            if (admin == null)
                throw new NotFoundException("Admin not found", "ADMIN_NOT_FOUND");

            return _mapper.Map<AdminProfileDto>(admin);
        }

        public async Task<string> RegisterAsync(RegisterAdminDto dto)
        {
            var exists = await _repository.GetByEmailAsync(dto.Email);

            if (exists != null)
                throw new BadRequestException(
                    "Email already exists",
                    "EMAIL_EXISTS"
                );

            var admin = _mapper.Map<Admin>(dto);

            admin.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            admin.Role = "Admin";
            admin.IsActive = true;
            admin.IsEmailVerified = false;
            admin.CreatedAt = DateTime.UtcNow;

            await _repository.AddAsync(admin);

            var token = Convert.ToBase64String(
                RandomNumberGenerator.GetBytes(64));

            var verificationLink =
                $"CampusHire Email Verification Token: {token}";

            await _emailService.SendEmailAsync(
                admin.Email,
                "CampusHire Email Verification",
                verificationLink
            );

            await _logger.Log(
                admin.AdminId,
                "CREATE_ADMIN",
                "New admin registered and verification email sent"
            );

            return "Admin registered successfully. Please verify email.";
        }

        public async Task<string> UpdateProfileAsync(int id, UpdateAdminProfileDto dto)
        {
            var admin = await _repository.GetByIdAsync(id);

            if (admin == null)
                throw new NotFoundException(
                    "Admin not found",
                    "ADMIN_NOT_FOUND"
                );

            admin.FullName = dto.FullName ?? admin.FullName;
            admin.Email = dto.Email ?? admin.Email;

            await _repository.UpdateAsync(admin);

            await _logger.Log(
                admin.AdminId,
                "UPDATE_PROFILE",
                "Admin profile updated"
            );

            return "Profile updated successfully";
        }

        public async Task ChangePasswordAsync(int id, ChangePasswordDto dto)
        {
            var admin = await _repository.GetByIdAsync(id);

            if (admin == null)
                throw new NotFoundException("Admin not found", "ADMIN_NOT_FOUND");

            if (!BCrypt.Net.BCrypt.Verify(dto.CurrentPassword, admin.PasswordHash))
                throw new BadRequestException(
                    "Current password is incorrect",
                    "INVALID_PASSWORD"
                );

            admin.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);

            await _repository.UpdateAsync(admin);

            await _logger.Log(
                admin.AdminId,
                "CHANGE_PASSWORD",
                "Admin password changed"
            );
        }

        public async Task<string> UpdateRoleAsync(int id, string role)
        {
            var admin = await _repository.GetByIdAsync(id);

            if (admin == null)
                throw new NotFoundException("Admin not found", "ADMIN_NOT_FOUND");

            admin.Role = role;

            await _repository.UpdateAsync(admin);

            return "Role updated successfully";
        }

        public async Task DeleteAsync(int id)
        {
            var admin = await _repository.GetByIdAsync(id);

            if (admin == null)
                throw new NotFoundException("Admin not found", "ADMIN_NOT_FOUND");

            await _repository.DeleteAsync(admin);
        }

        public async Task ActivateAsync(int id)
        {
            var admin = await _repository.GetByIdAsync(id);

            if (admin == null)
                throw new NotFoundException("Admin not found", "ADMIN_NOT_FOUND");

            admin.IsActive = true;

            await _repository.UpdateAsync(admin);

            await _logger.Log(
                admin.AdminId,
                "ACTIVATE_ADMIN",
                "Admin account activated"
            );
        }

        public async Task DeactivateAsync(int id)
        {
            var admin = await _repository.GetByIdAsync(id);

            if (admin == null)
                throw new NotFoundException("Admin not found", "ADMIN_NOT_FOUND");

            admin.IsActive = false;

            await _repository.UpdateAsync(admin);

            await _logger.Log(
                admin.AdminId,
                "DEACTIVATE_ADMIN",
                "Admin account deactivated"
            );
        }

        public async Task<string> ForgotPasswordAsync(ForgotPasswordDto dto)
        {
            var admin = await _repository.GetByEmailAsync(dto.Email);

            if (admin == null)
                throw new NotFoundException("Admin not found", "ADMIN_NOT_FOUND");

            var token = Guid.NewGuid().ToString("N");

            admin.ResetPasswordToken = token;
            admin.ResetPasswordTokenExpiry = DateTime.UtcNow.AddMinutes(30);

            await _repository.UpdateAsync(admin);

            await _emailService.SendEmailAsync(
                admin.Email,
                "CampusHire Password Reset",
                $"Your password reset token is: {token}"
            );

            return token;
        }

        public async Task<LoginResponseDto> RefreshTokenAsync(RefreshTokenDto dto)
        {
            var admin = await _repository.GetByRefreshTokenAsync(dto.RefreshToken);

            if (admin == null)
                throw new BadRequestException(
                    "Invalid refresh token",
                    "INVALID_REFRESH_TOKEN"
                );

            if (admin.RefreshTokenExpiryTime < DateTime.UtcNow)
                throw new BadRequestException(
                    "Refresh token expired",
                    "REFRESH_TOKEN_EXPIRED"
                );

            var accessToken = _jwtService.GenerateToken(admin);
            var refreshToken = _jwtService.GenerateRefreshToken();

            await _repository.UpdateRefreshTokenAsync(
                admin.AdminId,
                refreshToken,
                DateTime.UtcNow.AddDays(7)
            );

            return new LoginResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        public async Task ResetPasswordAsync(ResetPasswordDto dto)
        {
            var admin = await _repository.GetByResetTokenAsync(dto.Token);

            if (admin == null)
                throw new BadRequestException(
                    "Invalid reset token",
                    "INVALID_RESET_TOKEN"
                );

            if (admin.ResetPasswordTokenExpiry < DateTime.UtcNow)
                throw new BadRequestException(
                    "Reset token expired",
                    "RESET_TOKEN_EXPIRED"
                );

            admin.PasswordHash = PasswordHelper.HashPassword(dto.NewPassword);
            admin.ResetPasswordToken = null;
            admin.ResetPasswordTokenExpiry = null;

            await _repository.UpdateAsync(admin);
        }

        public async Task<AdminProfileDto> GetProfileAsync(int id)
        {
            var admin = await _repository.GetByIdAsync(id);

            if (admin == null)
                throw new NotFoundException("Admin not found", "ADMIN_NOT_FOUND");

            return new AdminProfileDto
            {
                AdminId = admin.AdminId,
                FullName = admin.FullName,
                Email = admin.Email,
                Role = admin.Role,
                IsActive = admin.IsActive
            };
        }
    }
}