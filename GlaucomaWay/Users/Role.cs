using Microsoft.AspNetCore.Identity;

namespace GlaucomaWay.Users;

public class Role : IdentityRole
{
    public const string Admin = nameof(Admin);
    public const string User = nameof(User);
}