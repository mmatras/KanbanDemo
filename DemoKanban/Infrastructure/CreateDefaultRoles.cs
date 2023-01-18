using Microsoft.AspNetCore.Identity;

namespace DemoKanban.Infrastructure
{
    public class CreateDefaultRoles
    {
        public static async Task CreateRoles(RoleManager<IdentityRole> roleManager)
        {
            foreach (var roleName in Enum.GetNames<UserRole>())
            {
                if(!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }

        public static async Task CreateUser(UserManager<IdentityUser> userManager, 
            IConfiguration configuration)
        {
            await userManager.CreateAsync(new IdentityUser
            {
                UserName = "admin",
                Email = "admin@admin.pl",
            }, configuration["adminPassword"]);
        }
    }
}
