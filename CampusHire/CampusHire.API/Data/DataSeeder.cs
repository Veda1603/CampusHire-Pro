using CampusHire.API.Entities;
using CampusHire.API.Helpers;

namespace CampusHire.API.Data
{
    public static class DataSeeder
    {
        public static void Seed(CampusHireDbContext context)
        {
            if (!context.Admins.Any())
            {
                context.Admins.Add(new Admin
                {
                    FullName = "System Administrator",
                    Email = "admin@campushire.com",
                    PasswordHash = PasswordHelper.HashPassword("Admin@123"),
                    Role = "Admin",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                });

                context.SaveChanges();
            }
        }
    }
}