using Models;

public class Opiniones
{
    public int Id { get; set; }
    public int IdUsuario { get; set; }
    public int IdPelicula { get; set; }
    public string Comentario { get; set; }
    public DateTime FechaComentario { get; set; }


    public Opiniones(int id, int idUsuario, int idPelicula, string comentario, DateTime fechaComentario)
    {
        Id = id;
        IdUsuario = idUsuario;
        IdPelicula = idPelicula;
        Comentario = comentario;
        FechaComentario = fechaComentario;
    }

}