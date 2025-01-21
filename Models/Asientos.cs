public class Asientos
{
    public string IdAsiento { get; set; }
    public int Numero { get; set; }
    public bool Estado { get; set; }
    public double Precio {get; set;}

    public Asientos(string idAsiento, int numero, bool estado, double precio)
    {
        IdAsiento = idAsiento;
        Numero = numero;
        Estado = estado;
        Precio = precio;
    }
}