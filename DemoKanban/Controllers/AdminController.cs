using DemoKanban.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Security.Claims;

namespace DemoKanban.Controllers
{
    public class IdentityUserViewModel 
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public IdentityRole Role { get; set; }

    }

    public class AdminController : Controller
    {
        private readonly KanbanContext _context;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IAuthorizationService authorizationService;

        public AdminController(KanbanContext context, UserManager<IdentityUser> userManager,
            IAuthorizationService authorizationService)
        {
            this._context = context;
            this.userManager = userManager;
            this.authorizationService = authorizationService;
        }

        public async Task<IActionResult> Users()
        {
            var isAuthorized = await authorizationService.AuthorizeAsync(User, "MinimumAge");


            var users = (from u in _context.Users
                        join ur in _context.UserRoles on u.Id equals ur.UserId into urGourp
                        from m in urGourp.DefaultIfEmpty()
                        join r in _context.Roles on m.RoleId equals r.Id into roleGroup
                        from rg in roleGroup.DefaultIfEmpty()
                        select new IdentityUserViewModel
                        {
                            Id = u.Id,
                            Email = u.Email,
                            UserName = u.UserName,
                            Role = rg
                        }).ToList();

            foreach (var user in users)
            {
                var dobClaimValue = (await userManager.GetClaimsAsync(new IdentityUser(user.UserName) { 
                    Id = user.Id
                }))
                    .FirstOrDefault(c => c.Type == ClaimTypes.DateOfBirth)?.Value;

                if (dobClaimValue != null)
                {
                    user.DateOfBirth = DateTime.Parse(dobClaimValue);
                }
            }

            //var users = _context.Users
            //    .GroupJoin(_context.UserRoles, u => u.Id, ur => ur.UserId, (user, userRole) =>
            //    new
            //    {
            //        User = user,
            //        RoleIds = userRole
            //    })
            //    .SelectMany(el => el.RoleIds.DefaultIfEmpty(), (pair, roles) => new
            //    {
            //        User = pair.User,
            //        Roles = roles.RoleId
            //    }).ToList()
            //    .Select(pair => new IdentityUserViewModel
            //    {
            //        Id = pair.User.Id, 
            //        Email = pair.User.Email,
            //        UserName = pair.User.UserName,
            //        Roles = _context.Roles.Where(r => pair.Roles.Contains(r.Id)).ToList()
            //    });

            ViewData["availableRoles"] = new[]
                {
                    new IdentityRole {
                        Id = "",
                        Name = "",
                    }
                }.Concat(_context.Roles.ToList());


            return View(users);
        }

        public async Task<IActionResult> AssignRole(string userId, string roleName)
        {
            var user = userManager.Users.FirstOrDefault(u => u.Id == userId);

            if (user != null)
            {
                var currentRoles = await userManager.GetRolesAsync(user);
                if (currentRoles != null)
                {
                    await userManager.RemoveFromRolesAsync(user, currentRoles);
                }

                await userManager.AddToRoleAsync(user, roleName);
            }

            return RedirectToAction("Users");
        }

        public async Task<IActionResult> AssignDateOfBirth(string userId, DateTime dateOfBirth)
        {
            var user = userManager.Users.FirstOrDefault(u => u.Id == userId);

            if (user != null)
            {
                await userManager.AddClaimAsync(user, new Claim(ClaimTypes.DateOfBirth, dateOfBirth.ToString()));
            }

            return RedirectToAction("Users");
        }
    }
}
