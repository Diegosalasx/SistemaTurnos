using System;
using System.Collections.Generic;

namespace ST.webAPI.Data.Entities;

public partial class Horario
{
    public int HorarioId { get; set; }

    public int DiaId { get; set; }

    public TimeSpan Desde { get; set; }

    public TimeSpan Hasta { get; set; }

    public virtual Dia Dia { get; set; } = null!;

    public virtual ICollection<EspecialidadesMedicasHorario> EspecialidadesMedicasHorarios { get; } = new List<EspecialidadesMedicasHorario>();
}
