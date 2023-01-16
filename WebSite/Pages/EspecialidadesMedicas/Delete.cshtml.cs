using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebSite.Services;

namespace WebSite.Pages.EspecialidadesMedicas
{
    public class DeleteModel : PageModel
    {
        private readonly TurnosMedicosClient context;

        public DeleteModel(TurnosMedicosClient context)
        {
            this.context = context;
        }

        [BindProperty]
      public EspecialidadesMedica EspecialidadesMedica { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var especialidadesmedica = await context.EspecialidadesMedicasGETAsync(id);
            if (especialidadesmedica == null)
            {
                return NotFound();
            }
            EspecialidadesMedica = especialidadesmedica;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (id == null )
            {
                return NotFound();
            }

            await context.EspecialidadesMedicasDELETEAsync(id);


            return RedirectToPage("./Index");
        }
    }
}
