using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebSite.Services;

namespace WebSite.Pages.EspecialidadesMedicas
{
    public class EditModel : PageModel
    {
        private readonly TurnosMedicosClient context;

        public EditModel(TurnosMedicosClient context)
        {
            this.context = context;
        }

        [BindProperty]
        public EspecialidadesMedica EspecialidadesMedica { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null )
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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await context
                .EspecialidadesMedicasPUTAsync(EspecialidadesMedica.EspecialidadMedicaId, 
                                                                        EspecialidadesMedica);

            return RedirectToPage("./Index");
        }

   
    }
}
