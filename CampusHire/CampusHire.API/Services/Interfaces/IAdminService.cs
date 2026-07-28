using CampusHire.API.DTOs.Admin;
using CampusHire.API.DTOs.Auth;

namespace CampusHire.API.Services.Interfaces
{
    public interface IAdminService
    {
        Task<List<AdminDto>> GetAllAsync();
        Task<AdminProfileDto?> GetByIdAsync(int id);
        Task<string> RegisterAsync(RegisterAdminDto dto);
        Task<string> UpdateProfileAsync(int id, UpdateAdminProfileDto dto);
        Task<string> UpdateRoleAsync(int id, string role);
        Task ChangePasswordAsync(int id, ChangePasswordDto dto);
        Task DeleteAsync(int id);
        Task ActivateAsync(int id);
        Task DeactivateAsync(int id);
        Task<LoginResponseDto> RefreshTokenAsync(RefreshTokenDto dto);
        Task<string> ForgotPasswordAsync(ForgotPasswordDto dto);
        Task ResetPasswordAsync(ResetPasswordDto dto);
        Task<AdminProfileDto> GetProfileAsync(int id);
    }
}