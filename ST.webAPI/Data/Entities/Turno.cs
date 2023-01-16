using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ST.webAPI.Data.Entities;

public partial class Turno
{
    public int TurnoId { get; set; }

    public int DiaId { get; set; }

    public TimeSpan Desde { get; set; }

    public TimeSpan Hasta { get; set; }


    public int EspecialidadMedicaId { get; set; }

    public string PacienteNombre { get; set; } = null!;

    public DateTime? Fecha { get; set; }

    public virtual Dia Dia { get; set; } = null!;    
    public virtual EspecialidadesMedica EspecialidadMedica { get; set; } = null!;
}
