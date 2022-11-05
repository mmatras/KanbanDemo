using Microsoft.AspNetCore.Mvc;

namespace DemoKanban.Areas.Documentation.Controllers
{
    [Area("Documentation")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
