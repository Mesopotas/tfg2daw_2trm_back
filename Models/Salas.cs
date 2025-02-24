using System.Collections.Specialized;
using System.Runtime.CompilerServices;

namespace Models;

public class Salas{

    public int IdSala{get; set;}
    public string  Nombre  {get; set;}
    public string  Capacidad {get; set;}
    public int  IdTipoSala {get; set;}
    public int  IdSedes {get; set;}
    public Salas(){} // CONTRUCTOR VACIO INYECCION DE DEPENDENCIAS

    public Salas(int idSala, string nombre, string capacidad, int idTipoSala, int idTipoSede){

        IdSala = idSala;
        Nombre = nombre;
        Capacidad = capacidad;
        IdTipoSala = idTipoSede;
    }


}