using GlaucomaWay.Users;
using Microsoft.AspNetCore.Identity;

namespace GlaucomaWay.Repositories;

public static class DataSeeder
{
    public static void SeedData(UserManager<User> userManager, RoleManager<Role> roleManager)
    {
        SeedRoles(roleManager);
        SeedUsers(userManager);
    }

    public static void SeedUsers(UserManager<User> userManager)
    {
        if (userManager.FindByNameAsync("Admin").Result == null)
        {
            var user = new User
            {
                UserName = "Admin",
                Email = "admin@localhost"
            };

            // TODO replace password
            var result = userManager.CreateAsync(user, "Admin123!").Result;

            if (result.Succeeded)
            {
                userManager.AddToRoleAsync(user, Role.Admin).Wait();
            }
        }
    }

    public static void SeedRoles(RoleManager<Role> roleManager)
    {
        if (!roleManager.RoleExistsAsync(Role.User).Result)
        {
            var role = new Role { Name = Role.User };
            _ = roleManager.CreateAsync(role).Result;
        }

        if (!roleManager.RoleExistsAsync(Role.Admin).Result)
        {
            var role = new Role { Name = Role.Admin };
            _ = roleManager.CreateAsync(role).Result;
        }
    }
}