using System;
using System.Collections.Generic;

namespace ST.webAPI.Data.Entities;

public partial class EspecialidadesMedica
{
    public int EspecialidadMedicaId { get; set; }

    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public bool Activo { get; set; }

    public DateTime Creacion { get; set; }

    public virtual ICollection<EspecialidadesMedicasHorario> EspecialidadesMedicasHorarios { get; } = new List<EspecialidadesMedicasHorario>();

    public virtual ICollection<Turno> Turnos { get; } = new List<Turno>();
}
