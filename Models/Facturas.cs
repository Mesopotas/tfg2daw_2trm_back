using Models;

public class Facturas
{
    public int Id { get; set; }
    public List<Usuarios> Usuarios { get; set; }
    public DateTime FechaFactura { get; set; } // pendiente de pensar si dejar DateTime o usar String
    public double Total { get; set; }
    public List<Sesiones> Sesiones { get; set; }
    public String AsientosComprados { get; set; }



    public Facturas(int id, List<Usuarios> usuarios, DateTime fechaFactura, double total, List<Sesiones> sesiones, String asientosComprados)
    {
        Id = id;
        Usuarios = usuarios;
        FechaFactura = fechaFactura;
        Total = total;
        Sesiones = sesiones;
        AsientosComprados = asientosComprados;
  
 
    }

}