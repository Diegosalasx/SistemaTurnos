using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using WebSite.Services;

namespace WebSite.Pages.Turnos
{
    public class IndexModel : PageModel
    {
        private readonly TurnosMedicosClient context;

        public IndexModel(TurnosMedicosClient context)
        {
            this.context = context;
        }

        [BindProperty]
        public List<SelectListItem> EspecialidadesMedicas { get; set; } = default!;
        [BindProperty]
        public int EspecialidadMedicaId { get; set; }

        public ICollection<Turno> Turno { get;set; } = default!;

        public async Task OnGetAsync(int especialidadMedicaId)
        {

            if (especialidadMedicaId == 0)
            {
                Turno = await context.TurnosAllAsync();
            }
            else
            {
                Turno = await context.GetTurnoByEspecialidadMedicaIdAsync(especialidadMedicaId);
            }
            

            var result = await context.EspecialidadesMedicasAllAsync();

            EspecialidadesMedicas = (from e in result
                                    select new SelectListItem()
                                    {
                                        Text = e.Nombre,
                                        Value = e.EspecialidadMedicaId.ToString(),
                                        Selected = e.EspecialidadMedicaId == especialidadMedicaId ? true: false
                                    }).ToList();

            EspecialidadesMedicas.Add(new SelectListItem()
            {
                Text = "seleccione",
                Value = "0",
                Selected = especialidadMedicaId == 0 ? true : false
            });
                                    

        }
    }
}
