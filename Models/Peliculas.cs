namespace Models;

public class Pelicula{

    public int IdPelicula{get; set;}
    public string Titulo {get; set;}
    public string Sinopsis {get; set;}    
    public string Duracion {get; set;}
    public string Categoria {get; set;}
    public double Director {get; set;}
    public double Anio {get; set;}
    public string ImagenURL {get; set;}
    public string Puntuacion {get; set;}

    public Pelicula(int idPelicula, string titulo, string sinopsis, string duracion, string categoria, double director, double anio, string imagenURL, string puntuacion){

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