namespace Models;

public class Reservas
{
    public int IdReserva { get; set; }
    public int IdUsuario { get; set; }
    public DateTime Fecha { get; set; }
    public string Descripcion { get; set; }

    public Reservas() { }

    public Reservas(int idReserva, int idUsuario, DateTime fecha, string descripcion)
    {
        IdReserva = idReserva;
        IdUsuario = idUsuario;
        Fecha = fecha;
        Descripcion = descripcion;
    }
}
