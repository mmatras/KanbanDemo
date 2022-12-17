using DemoKanban.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DemoKanban.Pages.Person
{
    public class CreateModel : PageModel
    {
        private readonly KanbanContext _context;

        public CreateModel(KanbanContext context)
        {
            this._context = context;
        }

        [BindProperty]
        public Models.Person Person { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }

            _context.People.Add(Person);
            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
