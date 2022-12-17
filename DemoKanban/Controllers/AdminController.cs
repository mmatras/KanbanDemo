using DemoKanban.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DemoKanban.Controllers
{
    public class IdentityUserViewModel 
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        public IEnumerable<IdentityRole> Roles { get; set; }

    }

    public class AdminController : Controller
    {
        private readonly KanbanContext _context;

        public AdminController(KanbanContext context)
        {
            this._context = context;
        }

        public IActionResult Users()
        {
            var users = _context.Users
                .GroupJoin(_context.UserRoles, u => u.Id, ur => ur.UserId, (user, userRole) =>
                new
                {
                    User = user,
                    RoleIds = userRole
                })
                .SelectMany(el => el.RoleIds.DefaultIfEmpty(), (pair, roles) => new
                {
                    User = pair.User,
                    Roles = roles.RoleId
                }).ToList()
                .Select(pair => new IdentityUserViewModel
                {
                    Id = pair.User.Id, 
                    Email = pair.User.Email,
                    UserName = pair.User.UserName,
                    Roles = _context.Roles.Where(r => pair.Roles.Contains(r.Id)).ToList()
                });

            ViewData["availableRoles"] = _context.Roles.ToList()
                .Concat(new[]
                {
                    new IdentityRole { 
                        Id = "",
                        Name = "",
                    }
                });
                

            return View(users);
        }
    }
}
