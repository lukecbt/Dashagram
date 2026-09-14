using Dashagram.Domain.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace Dashagram.Infrastructure.Identity
{
    public class AppUser : IdentityUser
    {
        public ICollection<Dog> Dogs { get; set; } = [];
    }
}
