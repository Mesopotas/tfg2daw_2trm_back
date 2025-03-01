using System.Collections.Generic;

namespace Models
{
    public class SalasDTO
    {
        public int IdSala { get; set; }
        public string Nombre { get; set; }
        public string URL_Imagen { get; set; }
        public int Capacidad { get; set; }
        public int IdTipoSala { get; set; }

        public int IdSede { get; set; }
        public bool Bloqueado { get; set; }
        public List<ZonasTrabajoDTO> Zona { get; set; } = new List<ZonasTrabajoDTO>();
        public List<PuestosTrabajoDTO> Puestos { get; set; } = new List<PuestosTrabajoDTO>();
    }

    public class ZonasTrabajoDTO
    {
        public int IdZonaTrabajo { get; set; }
        public string Descripcion { get; set; }
    }

    public class PuestosTrabajoDTO
    {
        public int IdPuestoTrabajo { get; set; }
        public string URL_Imagen { get; set; }
        public int CodigoMesa { get; set; }
        public bool Disponible { get; set; }
        public bool Bloqueado { get; set; }
        public List<DisponibilidadDTO> Disponibilidades { get; set; } = new List<DisponibilidadDTO>();
    }

    public class DisponibilidadDTO
    {
        public int IdDisponibilidad { get; set; }
        public int Fecha { get; set; }
        public bool Estado { get; set; }
        public int IdTramoHorario { get; set; }
    }
}