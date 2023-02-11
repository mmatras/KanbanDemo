using DemoKanban.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoKanban.Controllers
{
    public class PersonController : Controller
    {
        private readonly KanbanContext _context;

        public PersonController(KanbanContext kanbanContext)
        {
            _context = kanbanContext;
        }

        public IActionResult Index()
        {
            var people = _context.People.ToList();
            return View(people);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var issue = _context.People.FirstOrDefault(i => i.Id == id);

            if (issue == null)
            {
                return RedirectToAction("Error404", "Error");
            }

            return View(issue);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [FromForm] Person person)
        {
            if (id != person.Id)
            {
                return RedirectToAction("Error400", "Error");
            }

            if (!ModelState.IsValid)
            {
                return View(person);
            }

            var personToBeupdated = _context.People.FirstOrDefault(i => i.Id == id);

            if (personToBeupdated == null)
            {
                return RedirectToAction("Error400", "Error");
            }

            personToBeupdated.Name = person.Name;
            personToBeupdated.Surname = person.Surname;
            personToBeupdated.DateOfBirth = person.DateOfBirth;
            personToBeupdated.DisplayName = person.DisplayName;

            _context.People.Update(personToBeupdated);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
