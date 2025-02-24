namespace Models;

public class TipoSalas{

    public int IdTipoSala{get; set;}
    public string  Descripcion  {get; set;}
    public TipoSalas(){} // CONTRUCTOR VACIO INYECCION DE DEPENDENCIAS

    public TipoSalas(int idTipoSala, string descripcion){

        IdTipoSala = idTipoSala;
        Descripcion = descripcion;
    }


}