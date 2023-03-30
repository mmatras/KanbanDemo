using DemoKanban.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Mime;

namespace DemoKanban.Controllers
{
    public class FileUploadController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly KanbanContext kanbanContext;

        public FileUploadController(ILogger<HomeController> logger, KanbanContext kanbanContext)
        {
            _logger = logger;
            this.kanbanContext = kanbanContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Upload(IFormFile file)
        {
            using(var br = new BinaryReader(file.OpenReadStream()))
            {
                kanbanContext.Files.Add(new Models.File
                {
                    FileName = file.FileName,
                    ContentType = file.ContentType,
                    Content = br.ReadBytes((int)file.Length)
                });
                await kanbanContext.SaveChangesAsync();
            }

            return View("Index");
        }

        public IActionResult GetFile([FromQuery] int id)
        {
            var file = kanbanContext.Files.FirstOrDefault(f => f.Id == id);

            ContentDisposition cd = new ContentDisposition
            {
                FileName = file.FileName,
                Inline = true,
            };

            Response.Headers.Add("Content-Disposition", cd.ToString());
            Response.Headers.Add("X-Content-Type-Options", "nosniff");

            return File(file.Content, file.ContentType, file.FileName);
        }
    }
}