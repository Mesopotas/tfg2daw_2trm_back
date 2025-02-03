using Models;

public class GruposAsientos
{
    public int IdGruposSesiones { get; set; }
    public string Descripcion {get; set;}
    public double Precio {get; set;}



    public GruposAsientos(){}

    public GruposAsientos(int idGruposSesiones, string descripcion, double precio){

        IdGruposSesiones = idGruposSesiones;
        Descripcion = descripcion;
        Precio = precio;

    }

}