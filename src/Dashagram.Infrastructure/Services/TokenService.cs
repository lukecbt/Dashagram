using Dashagram.Application.Common.Interfaces.Services;
using Dashagram.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Dashagram.Infrastructure.Services
{
    public class TokenService(IConfiguration configuration, UserManager<ApplicationUser> userManager) : ITokenService
    {
        public async Task<string> GenerateTokenAsync(string userId, CancellationToken cancellationToken)
        {
            // Get validated user
            ApplicationUser user = await userManager.FindByIdAsync(userId);

            var claims = new List<System.Security.Claims.Claim>
            {
                new(System.Security.Claims.ClaimTypes.NameIdentifier, user.Id),
                new(System.Security.Claims.ClaimTypes.Email, user.Email)
            };

            // Add roles
            var roles = await userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Role, role));
            }

            // Add user claims
            var userClaims = await userManager.GetClaimsAsync(user);
            claims.AddRange(userClaims);

            // Generate token
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(double.Parse(configuration["Jwt:Duration"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
