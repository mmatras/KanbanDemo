using DemoKanban.Filters;
using DemoKanban.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DemoKanban.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/issue")]
    [ApiController]
    //[Authorize]
    [AuditLogFilter]
    public class IssueApiController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Issue> Get()
        {
            var issues = KanbanContext.Data.Issues;
            return issues;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var issue = KanbanContext.Data.Issues.FirstOrDefault(i => i.Id == id);
            return issue == null ? NotFound() : Ok(issue);
        }

        // POST api/issue
        [HttpPost]
        public IActionResult Post([FromBody] Issue issue)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (issue.AssignedToId != null)
            {
                issue.AssignedTo = KanbanContext.Data.People
                    .FirstOrDefault(m => m.Id == issue.AssignedToId, Person.Empty);
            }

            issue.Id = KanbanContext.Data.Issues.Max(i => i.Id) + 1;
            KanbanContext.Data.Issues.Add(issue);

            return Created($"/api/issue/{issue.Id}", issue);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Issue issue)
        {
            if (id != issue.Id)
                return BadRequest("id in url and in body doesn't match");

            var issueToBeUpdated = KanbanContext.Data.Issues.FirstOrDefault(i => i.Id == id);

            if (issueToBeUpdated == null)
            {
                return NotFound($"object with id:{id} doesn't exits.");
            }

            issueToBeUpdated.Title = issue.Title;
            issueToBeUpdated.State = issue.State;
            issueToBeUpdated.IsUrgent = issue.IsUrgent;
            issueToBeUpdated.Deadline = issue.Deadline;
            issueToBeUpdated.Notes = issue.Notes;

            return Ok();
        }

        // DELETE api/<IssueApiController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            KanbanContext.Data.Issues.RemoveAll(m => m.Id == id);

            return Ok();
        }
    }
}
