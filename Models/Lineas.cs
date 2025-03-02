namespace Models;

public class Lineas
{
    public int IdLinea { get; set; }
    public int IdReserva { get; set; }
    public int IdDetalleReserva { get; set; }
    public double PrecioTotal { get; set; }


    public Lineas() { }

    public Lineas(int idLinea, int idReserva, int idDetalleReserva, double precioTotal)
    {
        IdLinea = idLinea;
        IdReserva = idReserva;
        IdDetalleReserva = idDetalleReserva;
        PrecioTotal = precioTotal;
    }
}
