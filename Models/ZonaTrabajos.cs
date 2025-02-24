namespace Models;

public class ZonaTrabajos{

    public int IdZonaTrabajo{get; set;}
    public string  NumPuestoTrabajo  {get; set;}
    public string Descripcion {get; set;}
    public List<Salas> Sala  {get; set;}
    public ZonaTrabajos(){} // CONTRUCTOR VACIO INYECCION DE DEPENDENCIAS

    public ZonaTrabajos(int idZonaTrabajo, string numPuestoTrabajo, string descripcion, List<Salas> sala){
        IdZonaTrabajo = idZonaTrabajo;
        NumPuestoTrabajo = numPuestoTrabajo;
        Descripcion = descripcion;
        Sala = sala;
    }


}