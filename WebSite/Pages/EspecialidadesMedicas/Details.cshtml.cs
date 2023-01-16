using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebSite.Services;

namespace WebSite.Pages.EspecialidadesMedicas
{
    public class DetailsModel : PageModel
    {
        private readonly TurnosMedicosClient context;

        public DetailsModel(TurnosMedicosClient context)
        {
            this.context = context;
        }

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
    }
}
