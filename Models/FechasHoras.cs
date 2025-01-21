public class FechasHoras{
    
    public string IdFechaHora {set; get;} 
    public DateTime Fecha {set; get;}

    public FechasHoras(string idFechaHora, DateTime fecha){

        IdFechaHora = idFechaHora;
        Fecha = fecha;

    }

}