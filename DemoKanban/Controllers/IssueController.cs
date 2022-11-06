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
            ViewData["Action"] = "Create";
            ViewData["SubmitText"] = "Stwórz nowego";
            ViewData["People"] = GetPeopleSelectList();

            /*
             new CreateIssueViewModel
            {
                Issue = new Issue(),
                SelectListPeople = people
            }
             */

            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var issue = KanbanContext.Data.Issues.FirstOrDefault(i => i.Id == id);
            
            if(issue == null)
            {
                return RedirectToAction("Error404", "Error");
            }

            ViewData["Action"] = "Edit";
            ViewData["SubmitText"] = "Zapisz zmiany";
            ViewData["People"] = GetPeopleSelectList();

            return View(issue);
        }

        [HttpPost]
        public IActionResult Edit(int id, [FromForm] Issue issue)
        {
            if(id != issue.Id)
            {
                return RedirectToAction("Error400", "Error");
            }

            if(!ModelState.IsValid)
            {
                return View(issue);
            }

            var issueToBeUpdated = KanbanContext.Data.Issues.FirstOrDefault(i => i.Id == id);

            if(issueToBeUpdated == null)
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

        [HttpPost]
        public IActionResult Create([FromForm] Issue issue)
        {
            //if(KanbanContext.Data.Issues.Any(m => m.Title == issue.Title))
            //{
            //    ModelState.AddModelError("Title", "Takie zadanie już istnieje");
            //}

            if(!ModelState.IsValid)
            {
                ViewData["Action"] = "Create";
                ViewData["SubmitText"] = "Stwórz nowego";
                ViewData["People"] = GetPeopleSelectList();

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

        public IActionResult Delete(int id)
        {
            if (KanbanContext.Data.Issues.Any(i => i.Id == id))
            {
                TempData["errorMessage2"] = $"Brak zadanie o Id: {id}";
                return RedirectToAction("Index");
            }

            KanbanContext.Data.Issues.RemoveAll(m => m.Id == id);

            return RedirectToAction("Index");
        }

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
