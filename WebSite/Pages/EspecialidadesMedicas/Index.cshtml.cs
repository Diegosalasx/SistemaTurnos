using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebSite.Services;

namespace WebSite.Pages.EspecialidadesMedicas
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly TurnosMedicosClient context;

        public IndexModel(TurnosMedicosClient context)
        {
            this.context = context;
        }

        public ICollection<EspecialidadesMedica> EspecialidadesMedica { get;set; } = default!;

        public async Task OnGetAsync()
        {
            this.EspecialidadesMedica = await context.EspecialidadesMedicasAllAsync();
           
        }
    }
}
