using System.Collections.Specialized;
using System.Runtime.CompilerServices;

namespace Models;

public class PuestosTrabajo
{
    public int IdPuestoTrabajo { get; set; }
    public string URL_Imagen { get; set; }
    public int Codigo { get; set; }
    public int Estado { get; set; }
    public int Capacidad { get; set; }
    public int TipoPuesto { get; set; }
    public bool Bloqueado { get; set; }

    public PuestosTrabajo() { }

    public PuestosTrabajo(int idPuestoTrabajo, string urlImagen, int codigo, int estado, int capacidad, int tipoPuesto, bool bloqueado)
    {
        IdPuestoTrabajo = idPuestoTrabajo;
        URL_Imagen = urlImagen;
        Codigo = codigo;
        Estado = estado;
        Capacidad = capacidad;
        TipoPuesto = tipoPuesto;
        Bloqueado = bloqueado;
    }
}