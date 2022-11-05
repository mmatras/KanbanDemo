using Microsoft.AspNetCore.Mvc;

namespace DemoKanban.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index([FromRoute] string topic)
        {
            //query db

            return View("Index", topic);
        }
    }
}
