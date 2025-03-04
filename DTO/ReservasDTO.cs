public class ReservasDTO
{
    public int IdReserva { get; set; }
    public DateTime Fecha { get; set; }
    public string ReservaDescripcion { get; set; }
    public double PrecioTotal { get; set; }    
    // user info
    public int UsuarioId { get; set; } 
        public int DetallesReservasId { get; set; } 

    public string UsuarioNombre { get; set; }
    public string UsuarioEmail { get; set; }
    public List<DetalleReservaDTO> DetallesReservas { get; set; }
}

public class DetalleReservaDTO 
{
    public int IdDetalleReserva { get; set; }
    public int IdPuestoTrabajo { get; set; } 
    public string CodigoPuesto { get; set; }
    public string ImagenPuesto { get; set; }
    public string Descripcion { get; set; } 
}
