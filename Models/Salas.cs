using System.Collections.Specialized;
using System.Runtime.CompilerServices;

namespace Models;

public class Salas
{
    public int IdSala { get; set; }
    public string Nombre { get; set; }
    public string URL_Imagen { get; set; }
    public int Capacidad { get; set; }
    public int IdTipoSala { get; set; }
    public int IdSede { get; set; }
    public decimal Precio { get; set; }
    public bool Bloqueado { get; set; }
    public List<PuestosTrabajo> Puestos { get; set; }
    public List<ZonasTrabajo> Zona {get; set; }


    public Salas() { }

    public Salas(int idSala, string nombre, string urlImagen, int capacidad, int idTipoSala, int idSede, decimal precio, bool bloqueado = false)
    {
        IdSala = idSala;
        Nombre = nombre;
        URL_Imagen = urlImagen;
        Capacidad = capacidad;
        IdTipoSala = idTipoSala;
        IdSede = idSede;
        Precio = precio;
        Bloqueado = bloqueado;
    }
}