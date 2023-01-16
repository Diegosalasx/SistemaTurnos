using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using WebSite.Services;

namespace WebSite.Pages.EspecialidadesMedicas
{
    public class HorariosModel : PageModel
    {
       
        private readonly TurnosMedicosClient context;

        public HorariosModel(TurnosMedicosClient context)
        {
            this.context = context;
        }


        //public IActionResult OnGet()
        //{
        //ViewData["EspecialidadMedicaId"] = new SelectList(_context.EspecialidadesMedicas, "EspecialidadMedicaId", "EspecialidadMedicaId");
        //ViewData["HorarioId"] = new SelectList(_context.Horarios, "HorarioId", "HorarioId");
        //    return Page();
        //}

        [BindProperty]
        public EspecialidadesMedicasHorario EspecialidadesMedicasHorario { get; set; } = default!;
        [BindProperty]
        public EspecialidadesMedica EspecialidadesMedica { get; set; } = default!;
        [BindProperty]
        public ICollection<HorarioView>  HorarioViews { get; set; } = default!;

        [BindProperty]
        public List<int> GroupChecks { get; set; } = default!;
        [BindProperty]
        public int EspecialidadMedicaId { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            EspecialidadMedicaId = id;

            var especialidadesmedica = await context.EspecialidadesMedicasGETAsync(id);
            if (especialidadesmedica == null)
            {
                return NotFound();
            }
            EspecialidadesMedica = especialidadesmedica;

            var result = await context.HorariosAllAsync();

            var resultView = (from r in result
                              select new HorarioView()
                              {
                                  HorarioiID = r.HorarioId,
                                  NombreDia = r.Dia.NombreDia,
                                   Desde = r.Desde,
                                    Hasta= r.Hasta,
                                     IsChecked = false
                                     
                              }).ToList();

            HorarioViews = resultView;
            ICollection<EspecialidadesMedicasHorario> especialidaMedicaHorarios = await context.GetEspecialidadesMedicasHorarioByIdAsync(id);

            if (especialidaMedicaHorarios.Count > 0) {

                foreach (var horario in resultView) {

                    if (especialidaMedicaHorarios.Any(p => p.HorarioId == horario.HorarioiID))
                    { 
                        horario.IsChecked = true;
                    }

                }
            }



            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            await context.DeleteEspecialidadesMedicasHorarioByEspecialidadMedicaIdAsync(EspecialidadMedicaId);
            //GroupChecks delete 
            await context.PostEspecialoidadesMedicasAndHorariosAsync(EspecialidadMedicaId, GroupChecks);

            return RedirectToPage("./Index");
        }
    }

    public class HorarioView
    {
        public int EspecialidadMedicaHorarioID { get; set; }
        public int EspecialidadMedicaID { get; set; }
        public int HorarioiID { get; set; }
        public string NombreDia { get; set; }
        public string Desde { get; set; }
        public string Hasta { get; set; }
        public bool IsChecked { get; set; }


    }
}
