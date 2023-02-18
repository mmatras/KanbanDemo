﻿using DemoKanban.Filters;
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
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [AuditLogFilter]
    public class IssueApiController : ControllerBase
    {
        private readonly KanbanContext context;

        public IssueApiController(KanbanContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IEnumerable<IssueDto> Get([FromQuery] string? query)
        {
            //var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);

            IEnumerable<Issue> issues;
            if (query == null)
            {
                issues = context.Issues;
            } 
            else
            {
                issues = context.Issues.Where(issue => issue.Title.StartsWith(query));  
            }

            List<IssueDto> result = issues.ToList().Select(i => new IssueDto
            {
                Id = i.Id,
                Title = i.Title,
                Notes = i.Notes,
                State = i.State,
                IsUrgent = i.IsUrgent,
                Deadline = i.Deadline,
                AssignedToId = i.AssignedToId,
                AssignedPersonDisplayName = i.AssignedTo == null ? "" : 
                    string.IsNullOrEmpty(i.AssignedTo.DisplayName) ? $"{i.AssignedTo.Name} {i.AssignedTo.Surname}" : 
                    i.AssignedTo.DisplayName
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
        public IActionResult Post([FromBody] EditIssueDto issue)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            context.Issues.Add(new Issue
            {
                Title = issue.Title,
                Notes = issue.Notes,
                Deadline = issue.Deadline,
                IsUrgent = issue.IsUrgent,
                AssignedToId = issue.AssignedToId,
                State = issue.State
            });
            context.SaveChanges();

            return Created($"/api/issue/{issue.Id}", issue);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] EditIssueDto issue)
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
            issueToBeUpdated.AssignedToId = issue.AssignedToId;

            context.Issues.Update(issueToBeUpdated);
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
