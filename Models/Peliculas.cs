namespace Models;

public class Peliculas{

    public int IdPelicula{get; set;}
    public string Titulo {get; set;}
    public string Sinopsis {get; set;}    
    public int Duracion {get; set;}
    public string Categoria {get; set;}
    public string Director {get; set;}
    public int Anio {get; set;}
    public string ImagenURL {get; set;}
    public int Puntuacion {get; set;}


public Peliculas(){} /* El constructor sin parametros sirve para poder crear un objeto nuevo con los datos pertinentes sin tener que inicializar los datosen cada metodo, en este caso sustituye a 
(reader.GetInt32(0),reader.GetString(1),reader.GetString(2),(double)reader.GetDecimal(3),reader.GetString(4),reader.GetString(5),reader.GetDateTime(6),reader.GetString(7),reader.GetInt32(8))*/
    public Peliculas(int idPelicula, string titulo, string sinopsis, int duracion, string categoria, string director, int anio, string imagenURL, int puntuacion){

        IdPelicula = idPelicula;
        Titulo = titulo;
        Sinopsis = sinopsis;
        Duracion = duracion;
        Categoria = categoria;
        Director = director;
        Anio = anio;
        ImagenURL = imagenURL;
        Puntuacion = puntuacion;

    }
}