using DemoKanban.Models;
using Microsoft.AspNetCore.Mvc;

namespace DemoKanban.Controllers
{
    public class PersonController : Controller
    {
        public IActionResult Index()
        {
            var people = KanbanContext.Data.People;

            return View(people);
        }
    }
}
