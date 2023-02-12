using DemoKanban.Filters;
using DemoKanban.Models;
using DemoKanban.Models.Dto;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DemoKanban.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/person")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [AuditLogFilter]
    public class PersonApiController : ControllerBase
    {
        private readonly KanbanContext context;

        public PersonApiController(KanbanContext context)
        {
            this.context = context;
        }

        [HttpGet("personSelect")]
        public async Task<IEnumerable<PersonSelectDto>> GetPersonSelect()
        {
            var result = (await context.People.ToListAsync())
                .Select(p => new PersonSelectDto(p.Id,
                    string.IsNullOrEmpty(p.DisplayName) ? $"{p.Name} {p.Surname}" : p.DisplayName
                ));

            return result;
        }
    }
}
