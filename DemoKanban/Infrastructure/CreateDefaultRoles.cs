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
    }
}
