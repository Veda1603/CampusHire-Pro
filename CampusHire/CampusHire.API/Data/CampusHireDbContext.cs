using CampusHire.API.Entities;
using Microsoft.EntityFrameworkCore;
using CampusHire.API.Models;

namespace CampusHire.API.Data
{
    public class CampusHireDbContext : DbContext
    {
        public CampusHireDbContext(DbContextOptions<CampusHireDbContext> options)
            : base(options)
        {
        }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<EmailVerificationToken> EmailVerificationTokens { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<PlacementDrive> PlacementDrives { get; set; }
        public DbSet<StudentVerification> StudentVerifications { get; set; }
        public DbSet<AdminActivityLog> AdminActivityLogs { get; set; }
        public DbSet<PasswordResetToken> PasswordResetTokens { get; set; }
        public DbSet<VerificationHistory> VerificationHistories { get; set; }
        public DbSet<EligibilityCriteria> EligibilityCriterias { get; set; }
        public DbSet<RegistrationDeadline> RegistrationDeadlines { get; set; }
        public DbSet<PlacementResult> PlacementResults { get; set; }
    }
}