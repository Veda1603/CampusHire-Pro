using CampusHire.API.Data;
using CampusHire.API.DTOs.Admin;
using CampusHire.API.Helpers;
using CampusHire.API.Interfaces;
using CampusHire.API.Models;
using CampusHire.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace CampusHire.API.Services.Implementations;
public class PasswordResetService : IPasswordResetService
{
    private readonly CampusHireDbContext _context;
    private readonly IEmailService _emailService;
   public PasswordResetService(
        CampusHireDbContext context,
        IEmailService emailService)
    {
        _context = context;
        _emailService = emailService;
    }
   public async Task<string> GenerateResetToken(ForgotPasswordDto dto)
    {
        var admin = await _context.Admins
            .FirstOrDefaultAsync(x => x.Email == dto.Email);
        if (admin == null)
            throw new Exception("Admin not found");
        var token = Convert.ToBase64String(
            RandomNumberGenerator.GetBytes(64));
        var resetToken = new PasswordResetToken
        {
            AdminId = admin.AdminId,
            Token = token,
            ExpiryTime = DateTime.UtcNow.AddMinutes(30)
        };
        _context.PasswordResetTokens.Add(resetToken);
        await _context.SaveChangesAsync();
        await _emailService.SendEmailAsync(
            admin.Email,
            "CampusHire Password Reset",
            $"Your password reset token is: {token}"
        );
        return token;
    }
    public async Task<bool> ResetPassword(ResetPasswordDto dto)
    {
        var reset = await _context.PasswordResetTokens
            .Include(x => x.Admin)
            .FirstOrDefaultAsync(x => x.Token == dto.Token);
        if (reset == null || reset.IsUsed || reset.ExpiryTime < DateTime.UtcNow)
            return false;
        reset.Admin!.PasswordHash =
            PasswordHelper.HashPassword(dto.NewPassword);
        reset.IsUsed = true;
        await _context.SaveChangesAsync();
        return true;
    }
}