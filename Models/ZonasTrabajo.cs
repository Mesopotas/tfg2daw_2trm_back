using System.Collections.Specialized;
using System.Runtime.CompilerServices;

namespace Models;

public class ZonasTrabajo
{
    public int IdZonaTrabajo { get; set; }
    public int NumPuestosTrabajo { get; set; }
        public string Descripcion { get; set; }


    public ZonasTrabajo() { }

    public ZonasTrabajo(int idZonaTrabajo, int numPuestosTrabajo, string descripcion)
    {
        IdZonaTrabajo = idZonaTrabajo;
        NumPuestosTrabajo = numPuestosTrabajo;
        Descripcion = descripcion;

    }
}