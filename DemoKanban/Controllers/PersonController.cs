using DemoKanban.Models;
using Microsoft.AspNetCore.Mvc;

namespace DemoKanban.Controllers
{
    public class PersonController : Controller
    {
        private readonly KanbanContext _kanbanContext;

        public PersonController(KanbanContext kanbanContext)
        {
            _kanbanContext = kanbanContext;
        }

        public IActionResult Index()
        {
            var people = _kanbanContext.People.ToList();
            return View(people);
        }
    }
}
