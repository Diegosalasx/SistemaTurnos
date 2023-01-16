using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebSite.Services;

namespace WebSite.Pages.EspecialidadesMedicas
{
    public class CreateModel : PageModel
    {
        private readonly TurnosMedicosClient context;

        public CreateModel(TurnosMedicosClient context)
        {
            this.context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public EspecialidadesMedica EspecialidadesMedica { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid )
            {
                return Page();
            }

            this.EspecialidadesMedica.Creacion = DateTime.Now;
            await context.EspecialidadesMedicasPOSTAsync(this.EspecialidadesMedica);
            
            return RedirectToPage("./Index");
        }
    }
}
