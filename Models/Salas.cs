public class Salas
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public int Capacidad { get; set; }
    public List<Asientos> Asientos { get; set; }

    public Salas(int id, string nombre, int capacidad, List<Asientos> asientos)
    {
        Id = id;
        Nombre = nombre;
        Capacidad = capacidad;
        Asientos = asientos;
    }
}