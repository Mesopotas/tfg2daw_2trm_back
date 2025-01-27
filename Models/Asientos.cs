public class Asientos
{
    public int IdAsiento { get; set; }

    public int IdSala {get; set;}
    public int NumAsiento { get; set; }
    public bool Estado { get; set; }
    public double Precio {get; set;}


    public Asientos(){}

    public Asientos(int idAsiento, int idSala ,int numAsiento, bool estado, double precio)
    {
        IdAsiento = idAsiento;
        IdSala = idSala;
        NumAsiento = numAsiento;
        Estado = estado;
        Precio = precio;
    }
}