namespace Models;

public class FavoritosDTO
{
    public int IdFavorito { get; set; }
    public int IdUsuario { get; set; }
    public int IdSala { get; set; }
    public string NombreSala {get; set; }
    public string ImagenSala {get; set; }
    public int Capacidad {get; set; }
    public string TipoSala {get; set; }

}
