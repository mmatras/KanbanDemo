using DemoKanban.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DemoKanban.Pages.Person
{
    public class IndexModel : PageModel
    {
        private readonly KanbanContext _context;
        private ILogger<IndexModel> _logger;

        public IndexModel(KanbanContext context, ILogger<IndexModel> logger) => 
            (_context, _logger) = (context, logger);

        public IEnumerable<Models.Person> People { get; set; }

        public IActionResult OnGet(int? skip, int? take)
        {
            People = _context.People.Skip(skip ?? 0).Take(take ?? 100).ToList();

            return Page();
        }

        public IActionResult OnGetFirstPage()
        {
            People = _context.People.Take(100).ToList();
            return Page();
        }
    }
}
