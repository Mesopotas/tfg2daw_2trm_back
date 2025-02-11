namespace Models;

public class Salas{

    public int IdSala{get; set;}
    public string  Nombre {get; set;}
    public string Tipo {get; set;}
    public int Capacidad {get; set;}
    public double PrecioPorHora {get; set;}
    public int IdTipoSala {get;set;}


    public Salas(int idSala, string nombre, string tipo, int capacidad, double precioPorHora, int idTipoSala){
        IdSala = idSala;
        Nombre = nombre;
        Tipo = tipo;
        Capacidad = capacidad;
        PrecioPorHora = precioPorHora;
        IdTipoSala = idTipoSala;

    }

    public Salas(){}

}
