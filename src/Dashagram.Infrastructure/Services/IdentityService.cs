using Dashagram.Application.Common.Interfaces.Services;
using Dashagram.Application.DTOs.Users;
using Dashagram.Domain.Models.Entities;
using Dashagram.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace Dashagram.Infrastructure.Services
{
    public class IdentityService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager) : IIdentityService
    {
        public async Task<string> CreateUserAsync(CreateUserDto dto, CancellationToken cancellationToken)
        {
            var user = new ApplicationUser { Email = dto.Email, UserName = dto.Email };

            var result = await userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
            {
                throw new Exception("Failed to create user");
            }

            return user.Id;
        }

        public async Task<string?> ValidateUserAsync(LoginUserDto dto, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByEmailAsync(dto.Email);

            if (user is null)
            {
                return null;
            }

            var result = await signInManager.CheckPasswordSignInAsync(user, dto.Password, false);

            if (!result.Succeeded)
            {
                return null;
            }

            return user.Id;
        }
    }
}
