namespace Models;

public class Reservas{

    public int IdReserva {get; set;}
    public int IdUsuario  {get; set;}
    public int IdLinea  {get; set;}

    public Reservas(){}

    public Reservas(int idReserva, int idUsuario, int idLinea){

        IdReserva = idReserva;
        IdUsuario = idUsuario;
        IdLinea = idLinea;

    }


}