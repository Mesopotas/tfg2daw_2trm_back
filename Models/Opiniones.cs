using Models;

public class Opiniones
{
    public int Id { get; set; }
    public List<Usuarios> Usuarios { get; set; }
    public List<Peliculas> Peliculas { get; set; }
    public string Comentario { get; set; }
    public DateTime FechaComentario { get; set; } // pendiente de pensar si dejar DateTime o usar String


    public Opiniones(int id, List<Usuarios> usuarios, List<Peliculas> peliculas, string comentario, DateTime fechaComentario)
    {
        Id = id;
        Usuarios = usuarios;
        Peliculas = peliculas;
        Comentario = comentario;
        FechaComentario = fechaComentario;
    }

}