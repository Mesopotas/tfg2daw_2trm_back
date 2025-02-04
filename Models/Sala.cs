namespace Models;

public class Salas{

    public int IdSala{get; set;}
    public string  Nombre {get; set;}
    public int Capacidad {get; set;}
    public Salas(int idSala, string nombre,int capacidad){

        IdSala = idSala;
        Nombre = nombre;
        Capacidad = capacidad;

    }

    public Salas(){}

}
