using DemoKanban.Models;
using Microsoft.AspNetCore.Mvc;

namespace DemoKanban.Componets
{
    public class ListViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(IEnumerable<Person> people)
        {
            //people = people.Where(p => p.Name.StartsWith("M"));

            return View(people);
        }
    }
}
