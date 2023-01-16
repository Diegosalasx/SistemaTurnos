using System;
using System.Collections.Generic;

namespace ST.webAPI.Data.Entities;

public partial class Dia
{
    public int DiaId { get; set; }

    public string NombreDia { get; set; } = null!;

    public string NombreCorto { get; set; } = null!;

    public virtual ICollection<Horario> Horarios { get; } = new List<Horario>();

    public virtual ICollection<Turno> Turnos { get; } = new List<Turno>();
}
