using CampusHire.API.DTOs;
using CampusHire.API.DTOs.Admin;
namespace CampusHire.API.Interfaces;

public interface IPasswordResetService
{
    Task<string> GenerateResetToken(ForgotPasswordDto dto);
    Task<bool> ResetPassword(ResetPasswordDto dto);
}