using CampusHire.API.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CampusHire.API.Authentication
{
    public class JwtService
    {
        private readonly JwtSettings _settings;

        public JwtService(IOptions<JwtSettings> options)
        {
            _settings = options.Value;
        }

        public string GenerateToken(Admin admin)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, admin.AdminId.ToString()),
                new Claim(ClaimTypes.Name, admin.FullName),
                new Claim(ClaimTypes.Email, admin.Email),
                new Claim(ClaimTypes.Role, admin.Role)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_settings.Key));

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _settings.Issuer,
                audience: _settings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_settings.DurationInMinutes),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public string GenerateRefreshToken()
        {
            return Guid.NewGuid().ToString() + Guid.NewGuid().ToString();
        }
    }
}