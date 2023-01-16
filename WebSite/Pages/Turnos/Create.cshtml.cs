using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebSite.Models;
using WebSite.Services;

namespace WebSite.Pages.Turnos
{
    public class CreateModel : PageModel
    {
        private readonly TurnosMedicosClient context;

        public CreateModel(TurnosMedicosClient context)
        {
            this.context = context;
        }

        [BindProperty]
        public List<SelectListItem> RangeTime { get; set; } = default!;

        [BindProperty]
        public List<SelectListItem> EspecialidadMedica { get; set; } = default!;
   
        public async Task<IActionResult> OnGetAsync()
        {
            CreateRangeTime();

            var result= context.EspecialidadesMedicasAllAsync();
            EspecialidadMedica = result.Result.Select(p => new SelectListItem(p.Nombre, p.EspecialidadMedicaId.ToString())).ToList();

            return Page();
        }

        private void CreateRangeTime()
        {
            RangeTime = new List<SelectListItem>();
            RangeTime.Add(new SelectListItem("09:00", "09:00:00"));
            RangeTime.Add(new SelectListItem("09:30", "09:30:00"));
            RangeTime.Add(new SelectListItem("10:00", "10:00:00"));
            RangeTime.Add(new SelectListItem("10:30", "10:30:00"));
            RangeTime.Add(new SelectListItem("11:00", "11:00:00"));
            RangeTime.Add(new SelectListItem("11:30", "11:30:00"));
            RangeTime.Add(new SelectListItem("12:00", "12:00:00"));           
        }

        [BindProperty]
        public TurnoMedico Turno { get; set; }
        
        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {

            CreateRangeTime();

            var especilidades = context.EspecialidadesMedicasAllAsync();
            EspecialidadMedica = especilidades.Result.Select(p => new SelectListItem(p.Nombre, p.EspecialidadMedicaId.ToString())).ToList();


            //var numberDay = (int) Turno.Fecha.GetValueOrDefault().DayOfWeek;
            var numberDay = (int) Turno.Fecha.DayOfWeek;
            var horarios = await context.GetEspecialidadesMedicasHorarioByIdAsync(Turno.EspecialidadMedicaId);
            Turno.DiaId = numberDay;

            //Validción si el dia esta en la lista de horarios relacionados
            var result = (from d in horarios
                          select d.Horario.Dia.NombreCorto).ToList();
            if (!result.Any(p => p.Equals(numberDay.ToString())))
            {
                ModelState.AddModelError("Turno.Fecha", "El dia de la semana no se atiende para esta especilidad");
                return Page();
            };

            //se valida que el hasta sea mayor que desde

            var horaDesde = Convert.ToDateTime(Turno.Desde);
            var horaHasta = Convert.ToDateTime(Turno.Hasta);
            if(horaDesde >= horaHasta)
            {
                ModelState.AddModelError("Turno.Desde", "La fecha desde no puede ser igual o posterior a la fecha de hasta.");
                return Page();
            }

            if((horaHasta - horaDesde).Minutes != 30)
            {
                ModelState.AddModelError("Turno.Hasta", "El turnos no puede exceder los 30 minutos.");
                return Page();
            }

            if (String.IsNullOrEmpty(Turno.NombrePaciente))
            {
                ModelState.AddModelError("Turno.NombrePaciente", "Complete el paciente.");
                return Page();
            }

            var exist = await context.GetTurnoExistsAsync(Turno.DiaId, Turno.Desde, Turno.Hasta, Turno.EspecialidadMedicaId, Turno.Fecha);
            if (exist)
            {
                ModelState.AddModelError("Turno.NombrePaciente", "El turno para la especialidad, el dia y hora se encuentra ocupado.");                
                return Page();
            }

            Turno.DiaId = numberDay;
            await context.TurnosPOSTAsync(Turno.DiaId, Turno.Desde, Turno.Hasta, Turno.EspecialidadMedicaId, Turno.Fecha, Turno.NombrePaciente);
            return RedirectToPage("./Index");
        }
    }
}
