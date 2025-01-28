public class FechasHoras{
    
    public int IdFechaHora {set; get;} 
    public DateTime Fecha {set; get;}

    public FechasHoras(int idFechaHora, DateTime fecha){

        IdFechaHora = idFechaHora;
        Fecha = fecha;

    }

    public FechasHoras(){}
}