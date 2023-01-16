using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebSite.Services;

namespace WebSite.Pages.Turnos
{
    public class DeleteModel : PageModel
    {
        private readonly TurnosMedicosClient context;

        public DeleteModel(TurnosMedicosClient context)
        {
            this.context = context;
        }

        [BindProperty]
      public Turno Turno { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)

        {
            if (id == null )
            {
                return NotFound();
            }

            var turno = await context.TurnosGETAsync(id);

            if (turno == null)
            {
                return NotFound();
            }
            else 
            {
                Turno = turno;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            await context.TurnosDELETEAsync(id);

            return RedirectToPage("./Index");
        }
    }
}
