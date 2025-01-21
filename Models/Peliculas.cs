namespace Models;

public class Peliculas{

    public string IdPelicula{get; set;}
    public string Titulo {get; set;}
    public string Sinopsis {get; set;}    
    public string Duracion {get; set;}
    public string Categoria {get; set;}
    public string Director {get; set;}
    public DateTime Anio {get; set;}
    public string ImagenURL {get; set;}
    public double Puntuacion {get; set;}

    public Peliculas(string idPelicula, string titulo, string sinopsis, string duracion, string categoria, string director, DateTime anio, string imagenURL, double puntuacion){

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