namespace WebSite.Models
{
    public class TurnoMedico
    {
        public int TurnoId { get; set; }
        public int DiaId { get; set; }
        public string Desde { get; set; }
        public string Hasta { get; set; }
        public int EspecialidadMedicaId { get; set; }
        public string NombrePaciente { get; set; }
        public DateTime Fecha { get; set; }
    }
}
