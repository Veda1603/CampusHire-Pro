using CampusHire.API.Data;
using CampusHire.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CampusHire.API.Services.Implementations
{
    public class EmailVerificationService : IEmailVerificationService
    {
        private readonly CampusHireDbContext _context;

        public EmailVerificationService(CampusHireDbContext context)
        {
            _context = context;
        }

        public async Task<string> VerifyEmailAsync(string token)
        {
            var verification = await _context.EmailVerificationTokens
                .Include(x => x.Admin)
                .FirstOrDefaultAsync(x => x.Token == token);

            if (verification == null)
                return "Invalid verification token";

            if (verification.IsUsed)
                return "Token already used";

            if (verification.ExpiryTime < DateTime.UtcNow)
                return "Verification token expired";

            verification.IsUsed = true;

            if (verification.Admin != null)
                verification.Admin.IsEmailVerified = true;

            await _context.SaveChangesAsync();

            return "Email verified successfully";
        }
    }
}