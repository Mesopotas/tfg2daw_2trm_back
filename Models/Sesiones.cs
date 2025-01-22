using Models;

public class Sesiones
{
    public int Id { get; set; }
    public List<Peliculas> Peliculas { get; set; }
    public List<Salas> Salas { get; set; }
    public List<FechasHoras> FechasHoras { get; set; }

    public Sesiones(int id, List<Peliculas> peliculas, List<Salas> salas, List<FechasHoras> fechasHoras)
    {
        Id = id;
        Peliculas = peliculas;
        Salas = salas;
        FechasHoras = fechasHoras;
    }
}