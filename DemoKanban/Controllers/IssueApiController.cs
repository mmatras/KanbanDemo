using DemoKanban.Filters;
using DemoKanban.Models;
using DemoKanban.Models.Dto;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DemoKanban.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/issue")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [AuditLogFilter]
    public class IssueApiController : ControllerBase
    {
        private readonly KanbanContext context;

        public IssueApiController(KanbanContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IEnumerable<IssueDto> Get()
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);

            var issues = context.Issues;
            List<IssueDto> result = issues.Select(i => new IssueDto
            {
                Title = i.Title,
                /*...*/
            }).ToList();

            return result;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var issue = context.Issues.FirstOrDefault(i => i.Id == id);
            return issue == null ? NotFound() : Ok(issue);
        }

        // POST api/issue
        [HttpPost]
        public IActionResult Post([FromBody] IssueDto issue)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            context.Issues.Add(new Issue
            {
                Title = issue.Title,
            });
            context.SaveChanges();

            return Created($"/api/issue/{issue.Id}", issue);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Issue issue)
        {
            if (id != issue.Id)
                return BadRequest("id in url and in body doesn't match");

            var issueToBeUpdated = context.Issues.FirstOrDefault(i => i.Id == id);

            if (issueToBeUpdated == null)
            {
                return NotFound($"object with id:{id} doesn't exits.");
            }

            issueToBeUpdated.Title = issue.Title;
            issueToBeUpdated.State = issue.State;
            issueToBeUpdated.IsUrgent = issue.IsUrgent;
            issueToBeUpdated.Deadline = issue.Deadline;
            issueToBeUpdated.Notes = issue.Notes;

            context.Issues.Add(issueToBeUpdated);
            context.SaveChanges();

            return Ok();
        }

        //// DELETE api/<IssueApiController>/5
        //[HttpDelete("{id}")]
        //public IActionResult Delete(int id)
        //{
        //    KanbanContext.Data.Issues.RemoveAll(m => m.Id == id);

        //    return Ok();
        //}
    }
}
