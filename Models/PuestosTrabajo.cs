namespace Models;

public class PuestosTrabajo
{
    public int IdPuestoTrabajo { get; set; }
    public string URL_Imagen { get; set; }
    public int CodigoMesa { get; set; }
    public bool Disponible { get; set; }
    public bool Bloqueado { get; set; }

    public PuestosTrabajo() { }

    public PuestosTrabajo(int idPuestoTrabajo, string urlImagen, int codigoMesa, bool disponible, bool bloqueado)
    {
        IdPuestoTrabajo = idPuestoTrabajo;
        URL_Imagen = urlImagen;
        CodigoMesa = codigoMesa;
        Disponible = disponible;
        Bloqueado = bloqueado;
    }
}
