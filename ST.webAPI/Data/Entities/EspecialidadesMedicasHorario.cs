using System;
using System.Collections.Generic;

namespace ST.webAPI.Data.Entities;

public partial class EspecialidadesMedicasHorario
{
    public int EspecialidadMedicaHorarioId { get; set; }

    public int EspecialidadMedicaId { get; set; }

    public int HorarioId { get; set; }

    public virtual EspecialidadesMedica EspecialidadMedica { get; set; } = null!;

    public virtual Horario Horario { get; set; } = null!;
}
