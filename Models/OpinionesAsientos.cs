namespace Models;

public class OpinionesAsientos
{
        public int IdOpinionAsiento { get; set; }
        public string Opinion { get; set; }
        public DateTime FechaOpinion { get; set; }
        public int IdPuestoTrabajo { get; set; }
        public int IdUsuario { get; set; }
    public OpinionesAsientos() { }

    public OpinionesAsientos(int idOpinionAsiento, string opinion, DateTime fechaOpinion, int idPuestoTrabajo, int idUsuario)
    {
        IdOpinionAsiento = idOpinionAsiento;
        Opinion = opinion;
        FechaOpinion = fechaOpinion;
        IdPuestoTrabajo = idPuestoTrabajo;
        IdUsuario = idUsuario;
    }
}
