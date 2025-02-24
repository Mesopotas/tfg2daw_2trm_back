using System.Collections.Specialized;
using System.Runtime.CompilerServices;

namespace Models;

public class Salas{

    public int IdSala{get; set;}
    public string  Nombre  {get; set;}
    public int  Capacidad {get; set;}
    public int  IdTipoSala {get; set;}
    public int  IdSede {get; set;}
    public Salas(){} // CONTRUCTOR VACIO INYECCION DE DEPENDENCIAS

    public Salas(int idSala, string nombre, int capacidad, int idTipoSala, int idSede){

        IdSala = idSala;
        Nombre = nombre;
        Capacidad = capacidad;
        IdTipoSala = idTipoSala;
        IdSede = idSede;
    }


}