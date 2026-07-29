using CampusHire.API.Authentication;
using CampusHire.API.Data;
using CampusHire.API.DTOs.Auth;
using CampusHire.API.Entities;
using CampusHire.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CampusHire.API.Services.Implementations
{
    public class OtpService : IOtpService
    {
        private readonly CampusHireDbContext _context;
        private readonly IEmailService _emailService;
        private readonly JwtService _jwtService;

        public OtpService(
            CampusHireDbContext context,
            IEmailService emailService,
            JwtService jwtService)
        {
            _context = context;
            _emailService = emailService;
            _jwtService = jwtService;
        }

        public async Task<string> SendOtpAsync(SendOtpDto dto)
        {
            var admin = await _context.Admins.FirstOrDefaultAsync(x => x.Email == dto.Email);

            if (admin == null)
                return "Admin not found";

            var otp = new Random().Next(100000, 999999).ToString();

            var loginOtp = new LoginOtp
            {
                AdminId = admin.AdminId,
                OtpCode = otp,
                ExpiryTime = DateTime.UtcNow.AddMinutes(5)
            };

            _context.LoginOtps.Add(loginOtp);
            await _context.SaveChangesAsync();

            await _emailService.SendEmailAsync(
                admin.Email,
                "CampusHire Login OTP",
                $"Your OTP is {otp}. It is valid for 5 minutes.");

            return "OTP sent successfully";
        }

        public async Task<string> VerifyOtpAsync(VerifyOtpDto dto)
        {
            var admin = await _context.Admins.FirstOrDefaultAsync(x => x.Email == dto.Email);

            if (admin == null)
                return "Admin not found";

            var otp = await _context.LoginOtps
                .Where(x => x.AdminId == admin.AdminId)
                .OrderByDescending(x => x.CreatedAt)
                .FirstOrDefaultAsync();

            if (otp == null)
                return "OTP not found";

            if (otp.IsUsed)
                return "OTP already used";

            if (otp.ExpiryTime < DateTime.UtcNow)
                return "OTP expired";

            if (otp.OtpCode != dto.Otp)
                return "Invalid OTP";

            otp.IsUsed = true;
            await _context.SaveChangesAsync();

            return _jwtService.GenerateToken(admin);
        }
    }
}