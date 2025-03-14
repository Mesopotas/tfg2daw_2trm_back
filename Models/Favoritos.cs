namespace Models;

public class Favoritos
{
    public int IdFavorito { get; set; }
    public int IdUsuario { get; set; }
    public int IdSala { get; set; }


    public Favoritos() { }

    public Favoritos(int idFavorito, int idUsuario, int idSala)
    {
        IdFavorito = idFavorito;
        IdUsuario = idUsuario;
        IdSala = idSala;
    }
}
