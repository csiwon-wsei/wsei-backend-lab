using ApplicationCore.Models;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.EF.Entities;

public sealed class UserEntity: IdentityUser
{
    public UserDetails Details { get; set; }
}