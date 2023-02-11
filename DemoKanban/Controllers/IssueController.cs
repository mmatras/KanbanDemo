using DemoKanban.Filters;
using DemoKanban.Models;
using DemoKanban.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;

namespace DemoKanban.Controllers
{
    //public class CreateIssueViewModel
    //{
    //    public Issue Issue { get; set; }

    //    public IEnumerable<SelectListItem> SelectListPeople { get; set; }
    //}

    [AuditLogFilter]
    //[Authorize(Policy = "MinimumAge")]
    public class IssueController : Controller
    {
        private readonly KanbanContext _context;
        private readonly IStringLocalizer<IssueController> _stringLocalizer;
        private readonly IEmailService _emailService;

        public IssueController(IStringLocalizer<IssueController> stringLocalizer,
            IEmailService emailService, KanbanContext context)
        {
            _stringLocalizer = stringLocalizer;
            _emailService = emailService;
            _context = context;
        }


        [AuditLogFilter]
        //[OutputCache()]
        public IActionResult Index()
        {
            //var isAdmin = User.IsInRole("Admin");
            //User.HasClaim()

            //var session = Request?.HttpContext?.Session;
            //if(session != null)
            //{
            //    var xxx = session.Get("");
            //    //session.Set("abc", "");
            //}

            var issues = _context.Issues.ToList();

            return View(issues);
        }

        //[HttpGet]
        //public IActionResult Create()
        //{
        //    ViewData["Action"] = "Create";
        //    ViewData["SubmitText"] = _stringLocalizer["SubmitText_Create"];
        //    ViewData["People"] = GetPeopleSelectList();

        //    /*
        //     new CreateIssueViewModel
        //    {
        //        Issue = new Issue(),
        //        SelectListPeople = people
        //    }
        //     */

        //    return View();
        //}

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var issue = _context.Issues.FirstOrDefault(i => i.Id == id);

            if (issue == null)
            {
                return RedirectToAction("Error404", "Error");
            }

            ViewData["Action"] = "Edit";
            ViewData["SubmitText"] = _stringLocalizer["SubmitText_Edit"];
            ViewData["People"] = GetPeopleSelectList();

            return View(issue);
        }

        [HttpPost]
        public IActionResult Edit(int id, [FromForm] Issue issue)
        {
            if (id != issue.Id)
            {
                return RedirectToAction("Error400", "Error");
            }

            if (!ModelState.IsValid)
            {
                return View(issue);
            }

            var issueToBeUpdated = _context.Issues.FirstOrDefault(i => i.Id == id);

            if (issueToBeUpdated == null)
            {
                return RedirectToAction("Error400", "Error");
            }

            issueToBeUpdated.Title = issue.Title;
            issueToBeUpdated.State = issue.State;
            issueToBeUpdated.IsUrgent = issue.IsUrgent;
            issueToBeUpdated.Deadline = issue.Deadline;
            issueToBeUpdated.Notes = issue.Notes;

            return RedirectToAction("Index");
        }

        //[HttpPost]
        //public IActionResult Create([FromForm] Issue issue)
        //{
        //    //if(KanbanContext.Data.Issues.Any(m => m.Title == issue.Title))
        //    //{
        //    //    ModelState.AddModelError("Title", "Takie zadanie już istnieje");
        //    //}

        //    if(!ModelState.IsValid)
        //    {
        //        ViewData["Action"] = "Create";
        //        ViewData["SubmitText"] = _stringLocalizer["SubmitText_Create"];
        //        ViewData["People"] = GetPeopleSelectList();

        //        return View(issue);
        //    }

        //    if(issue.AssignedToId != null)
        //    {
        //        issue.AssignedTo = KanbanContext.Data.People
        //            .FirstOrDefault(m => m.Id == issue.AssignedToId, Person.Empty);
        //    }

        //    issue.Id = KanbanContext.Data.Issues.Max(i => i.Id) + 1;
        //    KanbanContext.Data.Issues.Add(issue);

        //    _emailService.Send("admin@comarch.pl", $"new issue {issue.Id} was created");

        //    return RedirectToAction("Index");
        //}

        //public IActionResult Delete(int id)
        //{
        //    if (KanbanContext.Data.Issues.Any(i => i.Id == id))
        //    {
        //        TempData["errorMessage2"] = $"Brak zadanie o Id: {id}";
        //        return RedirectToAction("Index");
        //    }

        //    KanbanContext.Data.Issues.RemoveAll(m => m.Id == id);

        //    return RedirectToAction("Index");
        //}

        private IEnumerable<SelectListItem> GetPeopleSelectList()
        {
            return KanbanContext.Data.People.Select(p =>
            {
                var displayName = p.DisplayName != "" ? $" ({p.DisplayName})" : "";

                return new SelectListItem($"{p.Name} {p.Surname}{displayName}", p.Id.ToString());
            });
        }
    }
}
