using DemoKanban.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DemoKanban.Controllers
{
    //public class CreateIssueViewModel
    //{
    //    public Issue Issue { get; set; }

    //    public IEnumerable<SelectListItem> SelectListPeople { get; set; }
    //}

    public class IssueController : Controller
    {
        public IActionResult Index()
        {
            var issues = KanbanContext.Data.Issues;

            return View(issues);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var people = KanbanContext.Data.People.Select(p =>
            {
                var displayName = p.DisplayName != "" ? $" ({p.DisplayName})" : "";


                return new SelectListItem($"{p.Name} {p.Surname}{displayName}", p.Id.ToString());
            });

            ViewData["People"] = people;

            /*
             new CreateIssueViewModel
            {
                Issue = new Issue(),
                SelectListPeople = people
            }
             */

            return View();
        }

        [HttpPost]
        public IActionResult Create([FromForm] Issue issue)
        {
            if(!ModelState.IsValid)
            {
                var people = KanbanContext.Data.People.Select(p =>
                {
                    var displayName = p.DisplayName != "" ? $" ({p.DisplayName})" : "";


                    return new SelectListItem($"{p.Name} {p.Surname}{displayName}", p.Id.ToString());
                });

                ViewData["People"] = people;

                return View(issue);
            }

            if(issue.AssignedToId != null)
            {
                issue.AssignedTo = KanbanContext.Data.People
                    .FirstOrDefault(m => m.Id == issue.AssignedToId, Person.Empty);
            }

            issue.Id = KanbanContext.Data.Issues.Max(i => i.Id) + 1;
            KanbanContext.Data.Issues.Add(issue);

            return RedirectToAction("Index");
        }
    }
}
