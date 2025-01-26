public class Salas
{
    public int IdSala { get; set; }
    public string Nombre { get; set; }
    public int Capacidad { get; set; }
    public List<Asientos> Asientos { get; set; }

    public Salas(int idSala, string nombre, int capacidad, List<Asientos> asientos)
    {
        IdSala = idSala;
        Nombre = nombre;
        Capacidad = capacidad;
        Asientos = asientos;
    }


    public Salas(){}

}