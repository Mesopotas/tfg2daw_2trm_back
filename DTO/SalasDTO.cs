using System.Collections.Generic;

namespace Models
{
    public class SalasDTO
    {
        public int IdSala { get; set; }
        public string Nombre { get; set; }
        public string URL_Imagen { get; set; }
        public int Capacidad { get; set; }
        public int IdSede { get; set; }
        public double Precio { get; set; }
        public bool Bloqueado { get; set; }

        public int NumeroMesas { get; set; }
        public int CapacidadAsientos { get; set; }
        public bool EsPrivada { get; set; }

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
    }


}